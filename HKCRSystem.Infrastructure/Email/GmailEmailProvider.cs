using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;

namespace HKCRSystem.Infrastructure.Email
{
    public class GmailEmailProvider : IGmailEmailProvider
    {
        private readonly string _from;
        private readonly SmtpClient _client;

        public GmailEmailProvider(IConfiguration configuration)
        {
            //gets the username from appsettings
            var userName = configuration.GetSection("GmailCredentials:UserName").Value!;

            _from = userName;
            //setting mailtrap 
            _client = new SmtpClient("sandbox.smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("bfc8faa9d3dadc", "ae4f057b38a757"),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(EmailMessage message)
        {
            //it sends the email to the user
            var mailMessage = new MailMessage(_from, message.To, message.Subject, message.Body);

            await _client.SendMailAsync(mailMessage);
        }
    }
}