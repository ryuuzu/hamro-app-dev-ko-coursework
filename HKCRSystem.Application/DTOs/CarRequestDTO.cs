using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class CarRequestDTO
    {
        public string? Company { get; set; }
        public string? Model { get; set; }
        public double Price { get; set; }
        public string? Status { get; set; }
    }
}
