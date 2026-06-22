using Microsoft.AspNetCore.Mvc;
using TodoApp.Context;
using TodoApp.Entities;
using TodoApp.Parameters;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    public class TodoController
    {
        public TodoService _service;
        public TodoContext _context;

        public TodoController(TodoService service, TodoContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        [Route("api/Todo")]
        public TodoResult<TodoEntity> Get(Pagination? pagination)
        {
            if (pagination == null)
            {
                pagination = new Pagination();
            }

            Task<TodoResult<TodoEntity>> result = _service.GetAllTodosAsync(pagination);

            return result.Result;
        }

        [HttpGet]
        [Route("api/Todo/{id}")]
        public TodoEntity Get(int id) 
        {
            TodoEntity todoEntity = TodoEntity.GetTodo(id, _context).Result;

            return todoEntity ?? new TodoEntity() 
            {
               Id = 0,
               Notes = "This id was not found"
            };
        }

        [HttpPut]
        [Route("api/Todo/{id}")]
        public TodoEntity? Put(int id, [FromBody] TodoEntity entity) 
        {
            Task<TodoEntity?> result = _service.UpdateTodoAsync(id, entity);

            return result.Result;
        }

        [HttpPost]
        [Route("api/Todo/")]

        public TodoEntity? Post([FromBody] TodoEntity entity) 
        {
            Task<TodoEntity> result = _service.CreateTodoAsync(entity);

            return result.Result;
        }

        [HttpPost]
        [Route("api/TodoList")]
        public List<TodoEntity> PostList([FromBody] List<TodoEntity> listOfTodos)
        {
            try
            {
                Task<List<TodoEntity>> result = _service.CreateListOfTodosAsync(listOfTodos);

                return result.Result;
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Todo/")]
        public int Delete() 
        {
            Task<int> result = _service.DeleteTodoAsync();

            return result.Result;
        }
    }
}
