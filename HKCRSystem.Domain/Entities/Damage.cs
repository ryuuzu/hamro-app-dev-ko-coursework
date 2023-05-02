using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Damage : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DamageDate { get; set; }
        public string? Description { get; set; }

        [ForeignKey("Request")] public Guid RequestId { get; set; }
        public virtual Request Request { get; set; }

        [ForeignKey("Billing")] public Guid BillingId { get; set; }
        public virtual Billing Billing{ get; set; }
    }
}
