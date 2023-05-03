using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using HKCRSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HKCRSystem.Infrastructure.Services
{
    public class PaymentService : IPayment
    {
        private readonly IApplicationDBContext _dbContext;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentService(IApplicationDBContext dContext, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _dbContext = dContext;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ResponseDTO> Payment(Guid billingId, string id)
        {
            //gets and checks the billing
            var billing = await _dbContext.Billings.FindAsync(billingId);
            if (billing == null)
                return new ResponseDTO
                {
                    Status = "Error",
                    Message = "No billing found."
                };

            //gets and checks the user
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new ResponseDTO
                {
                    Status = "Error",
                    Message = "No user found."
                };

            //gets the url of api 
            var url = _configuration.GetSection("App:ApiBaseUrl").Value!;

            //gets the price 
            float price = (float)((10f / 100) * billing.TotalPrice); 
            //if price is less than 10 sets to 10
            if (price < 10)
            {
                price = 10;
            }
            //data for Khalti
            var data = new
            {
                return_url = $"{url}/verify/payment",
                website_url = $"{url}",
                amount = price * 100,
                purchase_order_id = billingId,
                purchase_order_name = "Car Rent",
                customer_info = new
                {
                    name = $"{user.FirstName} {user.LastName}",
                    email = user.Email
                },
            };
            
            var client = new HttpClient();
            //authorization header of khalti
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Key", "a503a109720a45f48c8a49434deb5101");
            //hitting Post request
            var response = await client.PostAsJsonAsync("https://a.khalti.com/api/v2/epayment/initiate/", data);
            //validates the repsonse of khalti
            if (response.IsSuccessStatusCode)
            {
                //gets the content of response
                var responseData = await response.Content.ReadAsStringAsync();
                //returns the pidx and payment url of khalti
                return new ResponseDTO
                {
                    Status = "Success",
                    Message = responseData
                };
            }
            else
            {
                // Return an error message if the request was not successful
                return new ResponseDTO
                {
                    Status = "Error",
                    Message = "Server Error"
                };
            }
        }

        public async Task<ResponseDTO> PaymentVerify(string pidx, Guid billingId, int amount)
        {
            var data = new
                    {
                        pidx = pidx
                    };

            var client = new HttpClient();
            //authorization header of khalti
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Key", "a503a109720a45f48c8a49434deb5101");
            //hits POST request to validate payment status
            var response = await client.PostAsJsonAsync("https://a.khalti.com/api/v2/epayment/lookup/", data);
            if (response.IsSuccessStatusCode)
            {
                //reads the content
                var responseData = await response.Content.ReadAsStringAsync();

                //response data into Json Document object
                JsonDocument doc = JsonDocument.Parse(responseData);
                JsonElement root = doc.RootElement;
                //gets the value of status
                string transactionStatus = root.GetProperty("status").GetString();
                //validates the status
                if (transactionStatus == "Completed")
                {
                    //updates the blling
                    var billing = await _dbContext.Billings.FindAsync(billingId);
                    billing.AdvancePayment = amount / 100;

                    await _dbContext.SaveChangesAsync(default(CancellationToken));
                    return new ResponseDTO
                    {
                        Status = "Success",
                        Message = "No error"
                    };
                }
            }
            return new ResponseDTO
            {
                Status = "Error",
                Message = "Server error"
            };
        }
    }
}
