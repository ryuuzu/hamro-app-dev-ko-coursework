namespace HKCRSystem.Blazor.Data.DTO
{
    public class BillingData
    {
        public Guid Id { get; set; }
        public double TotalPrice { get; set; }
        public string PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public double AdvancePayment { get; set; }
    }
}
