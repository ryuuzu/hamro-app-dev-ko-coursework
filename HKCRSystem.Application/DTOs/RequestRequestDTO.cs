using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace HKCRSystem.Application.DTOs;

public class RequestRequestDTO
{
    [Required(ErrorMessage = "Requested Car Id is required")]
    public Guid RequestedCarId { get; set; }

    [Required(ErrorMessage = "Start Date is required")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "End Date is required")]
    public DateTime EndDate { get; set; }
}