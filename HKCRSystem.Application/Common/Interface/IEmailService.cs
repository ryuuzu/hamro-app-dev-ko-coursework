using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IEmailService
    {
        Task SendForgotPasswordEmailAsync(string name, string toEmail, string passwordResetToken);
        Task SendRequestAcceptedEmailAsync(string name, string toEmail, string carName);
    }
}
