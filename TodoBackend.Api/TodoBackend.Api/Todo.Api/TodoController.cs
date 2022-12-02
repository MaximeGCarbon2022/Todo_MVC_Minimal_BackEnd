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

    private TodoResponse MappingTodoResponseFrom(TodoModel todoModel)
    {
        if (todoModel is null)
            return null;

        return new TodoResponse(todoModel.Id, todoModel.Title, todoModel.Completed, todoModel.Order, "");
    }

}