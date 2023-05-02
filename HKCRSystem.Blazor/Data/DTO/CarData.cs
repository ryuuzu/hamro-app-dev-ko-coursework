namespace HKCRSystem.Blazor.Data.DTO
{
    public class CarData
    {
        public Guid? Id { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public double Price { get; set; }
        public string? Status { get; set; }
        public bool IsAvailable { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
