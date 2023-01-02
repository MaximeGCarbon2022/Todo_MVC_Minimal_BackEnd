using Dapper;
using Microsoft.AspNetCore.JsonPatch;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using TodoBackend.Api.Todo.Service;

namespace TodoBackend.Api.Todo.Data;

public class TodoDapperRepository : ITodoRepository
{
    private const string connectionId = "Default_Carbon";
    private const string schema = "[dbo]";
    private const string todoTableName = "[Todo]";

    private const string sql_management_order = $@"
;WITH CTE AS ( 
	SELECT [Id]
		  ,[Title]
		  ,[Completed]
		  ,[Order]
		  ,ROW_NUMBER() OVER(ORDER BY (SELECT 1)) as rowNumber
	  FROM {schema}.{todoTableName}
)

UPDATE {schema}.{todoTableName}
SET [Order] = rowNumber
FROM {schema}.{todoTableName} T
INNER JOIN CTE C ON C.Id = T.Id
";

    private readonly IConfiguration _configuration;

    public TodoDapperRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TodoModel> GetTodo(Guid id)
    {
        var sql = $@"
SELECT [Id],
       [Title],
       [Completed],
       [Order]
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
SELECT [Id],
       [Title],
       [Completed],
       [Order]
FROM {schema}.{todoTableName}
ORDER BY [Order]
";

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        IEnumerable<TodoEntity> todoEntity = await connection.QueryAsync<TodoEntity>(sql, new { });

        return todoEntity.Select(item => MappingTodoModelFrom(item)).ToList();
    }

    public async Task<TodoModel> CreateTodo(string title)
    {
        var sql = $@"
DECLARE @id VARCHAR(MAX) = (SELECT NEWID())
DECLARE @order INT = 0;

IF ((SELECT COUNT(1) FROM {schema}.{todoTableName}) > 0)
	SET @Order = (SELECT MAX([Order]) + 1 FROM {schema}.{todoTableName})

INSERT INTO {schema}.{todoTableName} ([Id], [Title], [Order])
VALUES (@id, @title, @order)

SELECT @id
";

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        string id = await connection.QuerySingleAsync<string>(sql, new { title });

        TodoModel TodoModel = await GetTodo(new Guid(id));
        return TodoModel;
    }

    public async Task<TodoModel> UpdateTodo(Guid id, string title, bool completed, int order)
    {
        var sql = $@"
UPDATE {schema}.{todoTableName}
SET [Title] = @title, 
    [Completed] = @completed, 
    [Order] = @order
WHERE [Id] = @id"
;
        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        await connection.ExecuteAsync(sql, new { id, title, completed, order });

        TodoModel todoModel = await GetTodo(id);
        return todoModel;
    }

    public async Task<TodoModel> UpdatePartialTodo(Guid id, string title, bool completed, int order)
    {
        var sql = $@"
UPDATE {schema}.{todoTableName}
SET [Title] = @title,
    [Completed] = @completed,
    [Order] = @order
WHERE [Id] = @id"
;

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        await connection.ExecuteAsync(sql, new { id, title, completed, order });

        TodoModel todoModel = await GetTodo(id);
        return todoModel;
    }

    public async Task<int> DeleteTodo(Guid id)
    {
        var sql = new StringBuilder();

        sql.Append($@"
DELETE FROM {schema}.{todoTableName}
WHERE Id = @id
");

        sql.Append(sql_management_order);

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        int result = await connection.ExecuteAsync(sql.ToString(), new { id });

        return result;
    }

    public async Task<int> DeleteTodos(bool? isCompleted)
    {
        var sql = new StringBuilder();

        sql.Append($@"
IF (@completed IS NULL)
	DELETE FROM {schema}.{todoTableName}
ELSE 
	DELETE FROM {schema}.{todoTableName}
	WHERE Completed = @completed
");
        sql.Append(sql_management_order);

        using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));
        int result = await connection.ExecuteAsync(sql.ToString(), new { completed = isCompleted });

        return result;
    }

    private TodoModel MappingTodoModelFrom(TodoEntity todoEntity)
    {
        if (todoEntity is null)
            return null;

        return new TodoModel(todoEntity.Id, todoEntity.Title, todoEntity.Completed, todoEntity.Order);
    }
}