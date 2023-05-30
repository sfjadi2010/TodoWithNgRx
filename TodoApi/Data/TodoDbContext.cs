using Microsoft.EntityFrameworkCore;

namespace TodoApi.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Task> Tasks => Set<Models.Task>();
    }
}