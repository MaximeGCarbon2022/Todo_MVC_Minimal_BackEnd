using Moq;
using TodoBackend.Api.Todo.Api;
using TodoBackend.Api.Todo.Service;
using TodoBackend.Api.UnitTest.Data;

namespace TodoBackend.Api.UnitTest
{
    public class TodoServiceTest
    {
        [Fact]
        public async Task GetTodos_Return_TodoList()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            IEnumerable<TodoModel> todosModel = TodoMockData.GetSampleTodosModel();

            todoService.Setup(_ => _.GetTodos())
                                    .ReturnsAsync(todosModel);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            var result = await controller.GetTodos();

            // Assert
            Assert.NotNull(result);
            result.Equals(todosModel);
        }

        [Fact]
        public async Task GetTodo_WithId_Return_Todo()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            TodoModel todoModel = TodoMockData.GetSampleTodoModel();
            IEnumerable<TodoModel> todosModel = TodoMockData.GetSampleTodosModel();

            todoService.Setup(_ => _.GetTodo(It.IsAny<Guid>()))
                                    .ReturnsAsync(todoModel);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            var result = await controller.GetTodo(todoModel.Id);

            // Assert
            Assert.NotNull(result);
            result.Equals(todosModel);
        }
    }
}