using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Data;

public class TodoDapperRepository : ITodoRepository
{
    public Task<TodoModel> GetTodo(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TodoModel>> GetTodos()
    {
        throw new NotImplementedException();
    }
}

