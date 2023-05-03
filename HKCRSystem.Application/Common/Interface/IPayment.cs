using HKCRSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.Common.Interface
{
    public interface IPayment
    {
        Task<ResponseDTO> Payment(Guid billingId, string userId);
        Task<ResponseDTO> PaymentVerify(string pidx, Guid billingId, int amount);
    }
}
