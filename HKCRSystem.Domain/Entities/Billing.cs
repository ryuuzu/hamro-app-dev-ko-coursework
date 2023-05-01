using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Billing : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double TotalPrice { get; set; }
        public string PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public double AdvancePayment { get; set; }
    }
}