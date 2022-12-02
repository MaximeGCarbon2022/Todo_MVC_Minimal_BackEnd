using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using TodoBackend.Api.Todo.Api.Model;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Api;

[ApiController]
[Route("/todos")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;
    private readonly LinkGenerator _linkGenerator;

    public TodoController(ITodoService service, LinkGenerator linkGenerator)
    {
        _service = service;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTodos()
    {
        IEnumerable<TodoModel> result = await _service.GetTodos();

        return Ok(result.Select(item => MappingTodoResponseFrom(item)).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodo(Guid id)
    {
        TodoModel todoModel = await _service.GetTodo(id);

        return Ok(MappingTodoResponseFrom(todoModel));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTodo(TodoCreationRequest todo)
    {
        TodoModel todoModel = await _service.CreateTodo(todo.Title);
        var todoResponse = MappingTodoResponseFrom(todoModel);

        return Created(todoResponse.Url, MappingTodoResponseFrom(todoModel));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateTodo(Guid id, TodoUpdateRequest todo)
    {
        TodoModel todoModel = await _service.UpdateTodo(id, todo.Title, todo.Completed, todo.Order);

        return Ok(MappingTodoResponseFrom(todoModel));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(Guid id)
    {
        await _service.DeleteTodo(id);

        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteTodos([FromQuery] bool? isCompleted)
    {
        await _service.DeleteTodos(isCompleted);

        return NoContent();
    }
    private TodoResponse MappingTodoResponseFrom(TodoModel todoModel)
    {
        if (todoModel is null)
            return null;

        var urlGenerator = _linkGenerator.GetUriByAction(
            HttpContext,
            nameof(GetTodo),
            "Todo",
            todoModel.Id.ToString()
           );

        return new TodoResponse(todoModel.Id, todoModel.Title, todoModel.Completed, todoModel.Order, urlGenerator);
    }

}