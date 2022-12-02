namespace TodoBackend.Api.Todo.Service;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetTodos();
    Task<TodoModel> GetTodo(Guid id);
}