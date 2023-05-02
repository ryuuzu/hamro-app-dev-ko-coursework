using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Return : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime ReturnDate { get; set; }

        [ForeignKey("Request")] public Guid RequestId { get; set; }
        public virtual Request Request { get; set; }

        [ForeignKey("AcceptBy")] public string? AcceptedBy { get; set; }
        public virtual ApplicationUser AcceptBy { get; set; }
    }
}
