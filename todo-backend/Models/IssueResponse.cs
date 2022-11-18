namespace todo_backend.Models
{
    public class IssueResponse
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Mail { get; set; }
    }
}
