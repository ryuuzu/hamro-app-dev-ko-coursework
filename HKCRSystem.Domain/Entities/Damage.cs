using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Damage : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public Guid BillingId { get; set; }
        public DateTime DamageDate { get; set; }
        public string? Description { get; set; }
    }
}
