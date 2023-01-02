using Microsoft.AspNetCore.JsonPatch;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Data
{
    public interface ITodoRepository
    {
        Task<IEnumerable<TodoModel>> GetTodos();
        Task<TodoModel> GetTodo(Guid id);
        Task<TodoModel> CreateTodo(string title);
        Task<TodoModel> UpdateTodo(Guid id, string title, bool completed, int order);
        Task<TodoModel> UpdatePartialTodo(Guid id, string title, bool completed, int order);
        Task<int> DeleteTodo(Guid id);
        Task<int> DeleteTodos(bool? isCompleted);
    }
}
