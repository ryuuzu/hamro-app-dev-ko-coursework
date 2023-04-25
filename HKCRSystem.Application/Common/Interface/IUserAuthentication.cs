using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IUserAuthentication
    {
        Task<ResponseDTO> Register(UserRegisterRequestDTO model);
        Task<ResponseDTO> Login(UserLoginRequestDTO model);
    }
}
