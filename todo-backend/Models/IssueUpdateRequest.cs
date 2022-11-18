namespace todo_backend.Models
{
    public class IssueUpdateRequest
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public int StatusId { get; set; }
        public string Mail { get; set; }
    }
}
