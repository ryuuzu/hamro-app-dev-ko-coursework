using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class OfferResponseDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Message { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Type { get; set; }

        public float DiscountPercent { get; set; }

        public Guid CreatedBy { get; set; }
    }
}