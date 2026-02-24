using Microsoft.EntityFrameworkCore;
using TodoApp.Context;
using TodoApp.Entities;
using TodoApp.Repositories;

namespace TodoApp.Services
{
    public class TodoService
    {
        TodoRepository _repository;
        IDbContextFactory<TodoContext> _contextFactory;
        TodoContext _context;
        public TodoService(TodoRepository repository, IDbContextFactory<TodoContext> contextFactory, TodoContext context)
        {
            _repository = repository;
            _contextFactory = contextFactory;
            _context = context;
        }
        public async Task<List<TodoEntity>> CreateListOfTodosAsync(List<TodoEntity> todoEntities)
        {
            TodoContext context = _contextFactory.CreateDbContext();
            List<TodoEntity> createdTodos = new();

            foreach (TodoEntity todo in todoEntities)
            {
                try
                {
                    TodoEntity? existingTodo = await _repository.GetByIdAsync(todo.Id);
                    if (existingTodo == null)
                    {
                        _repository.CreateAsync(todo, context).Wait();
                        createdTodos.Add(todo);
                    }
                    else
                    {
                        await context.DisposeAsync();
                        throw new Exception("One of the todos is already there");
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

            await context.SaveChangesAsync();

            return createdTodos;
        }

        public async Task<List<TodoEntity>> GetAllTodosAsync()
        {
            List<TodoEntity> result = await _repository.GetAllAsync();

            return result;
        }

        public async Task<TodoEntity?> GetTodoByIdAsync(int id)
        {
            TodoEntity? result = await _repository.GetByIdAsync(id);

            return result;
        }

        public async Task<TodoEntity?> UpdateTodoAsync(int id, TodoEntity updatedTodo)
        {
            TodoEntity? result = await _repository.UpdateAsync(id, updatedTodo);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<int> DeleteTodoAsync(int id)
        {
            int result = await _repository.DeleteAsync(id);

            await _context.SaveChangesAsync();

            return result;
        }

        public async Task<TodoEntity> CreateTodoAsync(TodoEntity todo)
        {
            TodoEntity result = await _repository.CreateAsync(todo);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
