namespace TodoBackend.Api.Todo.Service;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetTodos();
}

