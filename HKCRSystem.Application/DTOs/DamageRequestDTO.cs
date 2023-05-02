using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class DamageRequestDTO
    {
        public DateTime DamageDate { get; set; }
        public string Description { get; set; }
        public Guid RequestId { get; set; }
    }

    public class StaffDamageUpdateDTO
    {
        public double Price { get; set; }
    }
}
