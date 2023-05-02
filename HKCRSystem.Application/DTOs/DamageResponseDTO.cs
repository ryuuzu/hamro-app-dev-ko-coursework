using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class DamageResponseDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime DamageDate { get; set; }
        public string RequestedBy { get; set; }
        public DateTime ReportedDate { get; set; }
        public string DamagedCar { get; set; }
        public string ReviewedBy { get; set;}
        public double Price { get; set; }
    }
}
