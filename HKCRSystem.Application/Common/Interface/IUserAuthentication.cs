﻿using HKCRSystem.Application.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IUserAuthentication
    {
        Task<ResponseDTO> Register(UserRegisterRequestDTO model, IFormFile file);
        Task<ResponseDTO> Login(UserLoginRequestDTO model);
        Task<ResponseDTO> PasswordChange(string id, ChangePasswordDTO model);
        Task ForgotPassword(ResetPasswordDTO model);
        Task ResetPassword(ResetPasswordConfirmDTO model);
        Task PostAttachment(IFormFile file, string description, string type, string id);
        Task<ResponseDTO> ResetPasswordByAdmin(ResetPasswordAdmin model);
    }
}
