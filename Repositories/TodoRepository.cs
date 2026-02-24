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

        public async Task<TodoEntity?> UpdateAsync(int id, TodoEntity updatedTodo)
        {
            TodoEntity? existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null)
            {
                return null;
            }
            existingTodo.Notes = updatedTodo.Notes;

            return existingTodo;
        }

        public async Task<int> DeleteAsync(int id)
        {
            int result = await _context.Todos.Where(t => t.Id == id).ExecuteDeleteAsync();

            return result;
        }
    }
}
