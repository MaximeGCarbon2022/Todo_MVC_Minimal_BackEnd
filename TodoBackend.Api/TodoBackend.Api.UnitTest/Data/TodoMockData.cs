using System.Collections.Immutable;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.UnitTest.Data;

public class TodoMockData
{
    public static ImmutableList<TodoModel> GetSampleTodosModel()
    {
        ImmutableList<TodoModel> list = new List<TodoModel>
        {
            new TodoModel(Guid.NewGuid(), ".NET", false, 1),
            new TodoModel(Guid.NewGuid(), "C#", false, 2),
            new TodoModel(Guid.NewGuid(), "TypeScript", true, 3),
            new TodoModel(Guid.NewGuid(), "Dapper", true, 4)
        }.ToImmutableList();

        return list;
    }

    public static TodoModel GetSampleTodoModel()
        => new TodoModel(Guid.NewGuid(), "Software craftsmanship", false, 1);

    public static TodoModel GetSampleTodoModelModified(Guid id)
        => new TodoModel(id, "Agilité", false, 10);
}

