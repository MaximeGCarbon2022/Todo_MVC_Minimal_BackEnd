namespace TodoBackend.Api.Todo.Service
{
    public class TodoService : ITodoService
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
}
