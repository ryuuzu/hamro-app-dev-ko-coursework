namespace HKCRSystem.Application.DTOs;

public class BillingResponseDTO
{
    public Guid Id { get; set; }
    public double TotalPrice { get; set; }
    public string PaymentType { get; set; }
    public bool IsPaid { get; set; }
    public double AdvancePayment { get; set; }
}