using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Return : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public Guid? AcceptedBy { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
