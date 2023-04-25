using HKCRSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{    public interface ITokenService
    {
        string GenerateToken(ApplicationUser user);
    }
}
