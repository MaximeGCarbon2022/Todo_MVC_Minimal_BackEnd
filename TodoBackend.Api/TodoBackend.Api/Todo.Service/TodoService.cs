using TodoBackend.Api.Todo.Data;

namespace TodoBackend.Api.Todo.Service;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;

    public TodoService(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IEnumerable<TodoModel>> GetTodos()
    {
        IEnumerable<TodoModel> todosModel = await _todoRepository.GetTodos();

        return todosModel.Select(item => (item)).ToList();
    }

    public async Task<TodoModel> GetTodo(Guid id)
    {
        TodoModel todoModel = await _todoRepository.GetTodo(id);
        if (todoModel is null)
            throw new Exception($"The id: {id} was not found");

        return todoModel;
    }
}

