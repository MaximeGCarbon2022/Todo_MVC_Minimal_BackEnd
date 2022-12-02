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
            new TodoModel(Guid.NewGuid(), "TypeScript", false, 3)
        }.ToImmutableList();

        return list;
    }
}

