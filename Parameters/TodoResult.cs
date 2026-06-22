using TodoApp.Entities;
using TodoApp.Interfaces;

namespace TodoApp.Parameters
{
    public class TodoResult<Todo> : IPagedResult<Todo>
    {
        public int LastId { get; set; }
        public bool IsSuccess { get; set; }
        public List<Todo>? Data { get; set; }
    }
}
