using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float Discount { get; set; }
        public bool IsCancelled { get; set; } = false;

        [ForeignKey("RequestedCar")] public Guid RequestedCarId { get; set; }
        public virtual Car RequestedCar { get; set; }

        [ForeignKey("RequestedBy")] public string RequestedById { get; set; }
        public virtual ApplicationUser RequestedBy { get; set; }

        [ForeignKey("ApprovedBy")] public string ApprovedById { get; set; }
        public virtual ApplicationUser ApprovedBy { get; set; }

        [ForeignKey("Billing")] public Guid BillingId { get; set; }
        public virtual Billing Billing { get; set; }
    }
}