using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HKCRSystem.API.Controllers
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPayment _payment;
        private readonly IHelper _helper;
        private readonly IConfiguration _configuration;

        public PaymentController(IPayment payment, IHelper helper, IConfiguration configuration)
        {
            _payment = payment;
            _helper = helper;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/payment/{billingId}")]
        public async Task<ResponseDTO> MakePayment([FromRoute] Guid billingId)
        {
            //gets the id from the request header
            string id = _helper.GetIdFromToken(HttpContext);
            var result = await _payment.Payment(billingId, id);
            return result;
        }

        [HttpGet]
        [Route("api/verify/payment/")]
        public async Task<ResponseDTO> VerifyPayment([FromQuery] string pidx, [FromQuery] Guid purchase_order_id, [FromQuery] int amount)
        {
            var result = await _payment.PaymentVerify(pidx, purchase_order_id, amount);

            if (result.Status == "Success")
            {
                //gets the url from app settings and redirects to the billing page
                var url = _configuration.GetSection("App:WebAppBaseUrl").Value!;
                HttpContext.Response.Redirect($"{url}/billing", false);
            }
            return result;
        }
    }
}
