namespace todo_backend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;

        public int IssueId { get; set; }
        public Issue Issue { get; set; }
    }
}
