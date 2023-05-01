using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IGmailEmailProvider
    {
        Task SendEmailAsync(EmailMessage message);
    }
}
