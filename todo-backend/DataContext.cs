using Microsoft.EntityFrameworkCore;
using todo_backend.Models;

namespace todo_backend
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Status> Statuses { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
