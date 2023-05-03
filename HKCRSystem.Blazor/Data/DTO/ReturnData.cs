namespace HKCRSystem.Blazor.Data.DTO
{
    public class ReturnData
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime ReturnDate { get; set; }
        public string CarName { get; set; }
        public string UserName { get; set; }
    }
}
