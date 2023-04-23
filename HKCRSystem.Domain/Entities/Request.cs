using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestedCar { get; set; }
        public Guid RequestedBy { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Discount { get; set; }
        public Guid ApprovedBy { get; set; }
        public bool IsCancelled { get; set; } = false;
        public Guid BillingId { get; set; }
    }
}
