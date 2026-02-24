using Microsoft.EntityFrameworkCore;
using TodoApp.Entities;

namespace TodoApp.Context
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }
        public DbSet<TodoEntity> Todos { get; set; }
    }
}
