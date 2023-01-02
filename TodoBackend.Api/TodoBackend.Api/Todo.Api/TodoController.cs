using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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

        return Ok(result.Select(item => MappingTodoResponseFrom(item, nameof(GetTodos))).ToList());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodo(Guid id)
    {
        TodoModel todoModel = await _service.GetTodo(id);

        return Ok(MappingTodoResponseFrom(todoModel, nameof(GetTodo)));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTodo(TodoCreationRequest todo)
    {
        TodoModel todoModel = await _service.CreateTodo(todo.Title);
        var todoResponse = MappingTodoResponseFrom(todoModel, nameof(CreateTodo));

        return Created(todoResponse.Url, todoResponse);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateTodo(Guid id, TodoUpdateRequest todo)
    {
        TodoModel todoModel = await _service.UpdateTodo(id, todo.Title, todo.Completed, todo.Order);

        return Ok(MappingTodoResponseFrom(todoModel, nameof(UpdateTodo)));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialTodo(Guid id, [FromBody] JsonPatchDocument<TodoModel> patchDoc)
    {
        if (patchDoc != null)
        {
            var todoModel = await _service.GetTodo(id);
            if (todoModel != null)
            {
                patchDoc.ApplyTo(todoModel);
                await _service.UpdatePartialTodo(id, todoModel.Title, todoModel.Completed, todoModel.Order);
                return Ok(MappingTodoResponseFrom(todoModel, nameof(UpdateTodo)));
            }
        }
        return BadRequest();
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

    private TodoResponse MappingTodoResponseFrom(TodoModel todoModel, string action)
    {
        if (todoModel is null)
            return null;

        var urlGenerator = _linkGenerator.GetUriByAction(
            HttpContext,
            action,
            "Todo",
            todoModel.Id.ToString()
           );

        return new TodoResponse(todoModel.Id, todoModel.Title, todoModel.Completed, todoModel.Order, urlGenerator);
    }
}