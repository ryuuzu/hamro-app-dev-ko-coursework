using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HKCRSystem.Application.DTOs
{
    public class OfferRequestDTO
    {
        [Required(ErrorMessage = "Offer name is required.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Message is required.")]
        public string? Message { get; set; }
        
        [Required(ErrorMessage = "Start date is required.")]
        public DateTime StartDate { get; set; }
        
        [Required(ErrorMessage = "End date is required.")]
        public DateTime EndDate { get; set; }
        
        [Required(ErrorMessage = "Type is required.")]
        public string? Type { get; set; }
        
        [Required(ErrorMessage = "Discount percent is required.")]
        public float DiscountPercent { get; set; }
    }
}
