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

    public async Task<TodoModel> UpdateTodo(Guid id, string title, bool completed, int order)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be empty");

        var todos = await _todoRepository.GetTodos();

        if (!todos.Any(t => t.Id == id))
            throw new Exception($"The id: {id} was not found");

        if (todos.Any(t => t.Order == order))
            throw new Exception($"The order: {order} was already taken");

        TodoModel todoModel = await _todoRepository.UpdateTodo(id, title, completed, order);
        return todoModel;
    }


    public async Task<TodoModel> CreateTodo(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new Exception("Title cannot be empty");

        TodoModel todoModel = await _todoRepository.CreateTodo(title);
        return todoModel;
    }
}

