namespace TodoApp.Interfaces
{
    public interface IPagedResult<TEntity>
    {
        int LastId { get; set; }

        bool IsSuccess { get; set; }

        List<TEntity>? Data { get; set; }
    }
}
