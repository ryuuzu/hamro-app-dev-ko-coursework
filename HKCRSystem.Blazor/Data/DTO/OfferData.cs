namespace HKCRSystem.Blazor.Data.DTO
{
    public class OfferData
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Message { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Type { get; set; }

        public float DiscountPercent { get; set; }

        public string CreatedBy { get; set; }
    }
}
