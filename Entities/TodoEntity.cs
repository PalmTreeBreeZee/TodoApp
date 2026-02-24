using Microsoft.EntityFrameworkCore;
using TodoApp.Context;

namespace TodoApp.Entities
{
    public class TodoEntity
    {
        public int Id { get; set; }
        public string? Notes { get; set; }
        public bool? Checked { get; set; }
        public static async Task<TodoEntity> GetTodo(int id, TodoContext context)
        {
            if (context == null)
            {
                throw new Exception("Context is not set");
            }

            TodoEntity? result = await context.Todos.FindAsync(id);
            
            if (result == null) 
            {
                throw new Exception($"Todo with Id {id} not found");
            }

            return result;
        }
    }
}
