using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Data
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoModel>> GetTodos();
        Task<TodoModel> GetTodo(Guid id);
        Task<TodoModel> CreateTodo(string title);
    }
}
