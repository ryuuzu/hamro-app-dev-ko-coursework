using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IHelper
    {
        string GetIdFromToken(HttpContext context);
        ResponseDTO ValidateFile(IFormFile file);
    }
}
