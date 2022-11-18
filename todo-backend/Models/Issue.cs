namespace todo_backend.Models
{
    public class Issue
    {
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string Subject { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }

        public int StatusId { get; set; } = 1;
        public Status Status { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
