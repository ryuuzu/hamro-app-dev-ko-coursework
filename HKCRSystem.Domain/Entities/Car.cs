using HKCRSystem.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Domain.Entities
{
    public class Car : BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Company { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public string Status { get; set; }
        public bool IsAvailable { get; set; }
    }
}
