namespace Domain.DTOs.Requests
{
    public class CreateReportRequest
    {
        public int? AccountId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
