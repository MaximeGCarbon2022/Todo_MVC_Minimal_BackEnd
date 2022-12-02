using Moq;
using TodoBackend.Api.Todo.Data;
using TodoBackend.Api.Todo.Service;
using TodoBackend.Api.UnitTest.Data;

namespace TodoBackend.Api.UnitTest;

public class TodoRepositoryTest
{
    [Fact]
    public async Task GetTodos_Return_TodoList()
    {
        // Arrange
        var todoRepository = new Mock<ITodoRepository>();
        IEnumerable<TodoModel> todosModel = TodoMockData.GetSampleTodosModel();

        todoRepository.Setup(_ => _.GetTodos())
                                   .ReturnsAsync(todosModel);

        TodoService service = new TodoService(todoRepository.Object);

        // Act
        var result = await service.GetTodos();

        // Assert
        Assert.NotNull(result);
        result.Equals(todosModel);
    }

    [Fact]
    public async Task GetTodo_WithId_Return_Todo()
    {
        // Arrange
        var todoRepository = new Mock<ITodoRepository>();
        TodoModel todoModel = TodoMockData.GetSampleTodoModel();

        todoRepository.Setup(_ => _.GetTodo(It.IsAny<Guid>()))
                                   .ReturnsAsync(todoModel);

        TodoService service = new TodoService(todoRepository.Object);

        // Act
        var result = await service.GetTodo(todoModel.Id);

        // Assert
        Assert.NotNull(result);
        result.Equals(todoModel);
    }

    [Fact]
    public async Task CreateTodo_WithTitle_Return_Todo()
    {
        // Arrange
        var todoRepository = new Mock<ITodoRepository>();
        TodoModel todoModel = TodoMockData.GetSampleTodoModel();

        todoRepository.Setup(_ => _.CreateTodo(It.IsAny<string>()))
                                   .ReturnsAsync(todoModel);

        TodoService service = new TodoService(todoRepository.Object);

        // Act
        var result = await service.CreateTodo(todoModel.Title);

        // Assert
        Assert.NotNull(result);
        result.Equals(todoModel);
    }

    [Fact]
    public async Task UpdateTodo_WithTodo_Return_TodoModified()
    {
        // Arrange
        var todoRepository = new Mock<ITodoRepository>();
        IEnumerable<TodoModel> todosModel = TodoMockData.GetSampleTodosModel();
        TodoModel todoModel = TodoMockData.GetSampleTodoModelModified(todosModel.First().Id);

        todoRepository.Setup(_ => _.GetTodos())
                                   .ReturnsAsync(todosModel);

        todoRepository.Setup(_ => _.UpdateTodo(It.IsAny<Guid>(), It.IsAny<string>(),
                                               It.IsAny<bool>(), It.IsAny<int>()))
                                   .ReturnsAsync(todoModel);

        TodoService service = new TodoService(todoRepository.Object);

        // Act
        var result = await service.UpdateTodo(todoModel.Id, "Update Title", false, 10);

        // Assert
        Assert.NotNull(result);
        result.Equals(todoModel);
    }
}