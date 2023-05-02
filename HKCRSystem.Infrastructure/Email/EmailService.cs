using HKCRSystem.Application.Common.Interface;
using HKCRSystem.Application.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Infrastructure.Email
{
    public class EmailService : IEmailService
    {
        private readonly string _webAppBaseUrl;
        private readonly IGmailEmailProvider _emailProvider;

        public EmailService(IConfiguration configuration, IGmailEmailProvider emailProvider)
        {
            //gets the domain of web app
            _webAppBaseUrl = configuration.GetSection("App:WebAppBaseUrl").Value!;
            _emailProvider = emailProvider;
        }

        public async Task SendForgotPasswordEmailAsync(string name, string toEmail, string passwordResetToken)
        {
            //generates the link
            var passwordRestUrl = $"{_webAppBaseUrl}/reset-password?token={passwordResetToken}";
            //sets the message format
            var message = new EmailMessage
            {
                Subject = "Password Reset Request",
                To = toEmail,
                Body = @$"Dear {name},
                     To reset your password, please click on the following link:
                     {passwordRestUrl}"
            };

            //send the email
            await _emailProvider.SendEmailAsync(message);
        }

        public async Task SendRequestAcceptedEmailAsync(string name, string toEmail, string carName)
        {
            //sets the message format
            var message = new EmailMessage
            {
                Subject = "Rent Request Accepted",
                To = toEmail,
                Body = @$"Dear {name},
                     Your rent request for {carName} has been accepted."
            };

            //send the email
            await _emailProvider.SendEmailAsync(message);
        }
    }
}