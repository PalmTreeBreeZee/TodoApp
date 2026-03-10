using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Entities;

namespace TodoApp.Repositories
{
    public class TodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(IDbContextFactory<TodoContext> contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public async Task<List<TodoEntity>> GetAllAsync()
        {
            return await _context.Todos.ToListAsync();
        }

        public async Task<TodoEntity?> GetByIdAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }

        public async Task<TodoEntity> CreateAsync(TodoEntity todo, TodoContext? context = null, bool? tracking = null)
        {
            if (context == null)
            {
                _context.Todos.Add(todo);
            } 
            else
            {
                context.Todos.Add(todo);
            }

            return todo;
        }

        public async Task<TodoEntity?> UpdateAsync(int id, TodoEntity updatedTodo, TodoContext? context = null)
        {
            TodoEntity? existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null)
            {
                return null;
            }
            if (context != null)
            {
                existingTodo.Notes = updatedTodo.Notes;
                existingTodo.Checked = updatedTodo.Checked;
                context.Todos.Update(existingTodo);
            }

            return existingTodo;
        }

        public async Task<int> DeleteAsync(TodoContext context)
        {
            List<TodoEntity> todos = await context.Todos.Where(t => t.Checked == true).ToListAsync();

            context.RemoveRange(todos);

            return todos.Count;
        }
    }
}
