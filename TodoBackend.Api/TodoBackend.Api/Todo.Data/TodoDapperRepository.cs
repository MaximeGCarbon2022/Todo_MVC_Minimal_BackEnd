using Dapper;
using System.Data;
using System.Data.SqlClient;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Data;

public class TodoDapperRepository : ITodoRepository
{
    private const string connectionId = "Default_Carbon";
    private const string schema = "[dbo]";
    private const string todoTableName = "[Todo]";

    private readonly IConfiguration _configuration;

    public TodoDapperRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TodoModel> GetTodo(Guid id)
    {
        var sql = $@"
SELECT Id, Title, Completed, [Order]
FROM {schema}.{todoTableName}
WHERE Id = @id"
;
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        TodoEntity todoEntity = await connection.QueryFirstOrDefaultAsync<TodoEntity>(sql, new { id });

        return MappingTodoModelFrom(todoEntity);
    }

    public async Task<IEnumerable<TodoModel>> GetTodos()
    {
        var sql = $@"
SELECT Id, Title, Completed, [Order]
FROM {schema}.{todoTableName}
ORDER BY [Order]
";

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        IEnumerable<TodoEntity> todoEntity = await connection.QueryAsync<TodoEntity>(sql, new { });

        return todoEntity.Select(item => MappingTodoModelFrom(item)).ToList();
    }

    private TodoModel MappingTodoModelFrom(TodoEntity todoEntity)
    {
        if (todoEntity is null)
            return null;

        return new TodoModel(todoEntity.Id, todoEntity.Title, todoEntity.Completed, todoEntity.Order);
    }
}

