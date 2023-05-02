using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class CarResponseDTO
    {
        public Guid? Id { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public double Price { get; set; }
        public string? Status { get; set; }
        public bool IsAvailable { get; set; }
        public int TimesRented { get; set; }
        public Guid CreatedBy { get; set; }
    }
}