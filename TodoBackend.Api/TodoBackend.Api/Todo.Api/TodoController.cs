using Microsoft.AspNetCore.Mvc;
using TodoBackend.Api.Todo.Api.Model;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Api;

[ApiController]
[Route("/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetTodos()
    {
        IEnumerable<TodoModel> result = await _service.GetTodos();

        return Ok(result.Select(item => MappingTodoResponseFrom(item)).ToList());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTodo(Guid id)
    {
        TodoModel todoModel = await _service.GetTodo(id);

        return Ok(MappingTodoResponseFrom(todoModel));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodo(TodoCreationRequest todo)
    {
        TodoModel todoModel = await _service.CreateTodo(todo.Title);
        var todoResponse = MappingTodoResponseFrom(todoModel);

        return Created(todoResponse.Url, MappingTodoResponseFrom(todoModel));
    }

    private TodoResponse MappingTodoResponseFrom(TodoModel todoModel)
    {
        if (todoModel is null)
            return null;

        return new TodoResponse(todoModel.Id, todoModel.Title, todoModel.Completed, todoModel.Order, "");
    }

}