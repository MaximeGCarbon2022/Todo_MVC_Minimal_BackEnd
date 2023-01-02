using Microsoft.AspNetCore.JsonPatch;

namespace TodoBackend.Api.Todo.Service;

public interface ITodoService
{
    Task<IEnumerable<TodoModel>> GetTodos();
    Task<TodoModel> GetTodo(Guid id);
    Task<TodoModel> CreateTodo(string title);
    Task<TodoModel> UpdateTodo(Guid id, string title, bool completed, int order);
    Task<TodoModel> UpdatePartialTodo(Guid id, string title, bool completed, int order);
    Task<int> DeleteTodo(Guid id);
    Task<int> DeleteTodos(bool? isCompleted);
}