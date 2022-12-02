using Moq;
using TodoBackend.Api.Todo.Api;
using TodoBackend.Api.Todo.Api.Model;
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

            todoService.Setup(_ => _.GetTodo(It.IsAny<Guid>()))
                                    .ReturnsAsync(todoModel);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            var result = await controller.GetTodo(todoModel.Id);

            // Assert
            Assert.NotNull(result);
            result.Equals(todoModel);
        }

        [Fact]
        public async Task CreateTodo_WithTitle_Return_Todo()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            TodoModel todoModel = TodoMockData.GetSampleTodoModel();

            todoService.Setup(_ => _.CreateTodo(It.IsAny<string>()))
                                    .ReturnsAsync(todoModel);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            TodoCreationRequest todoRequest = new TodoCreationRequest(todoModel.Title);
            var result = await controller.CreateTodo(todoRequest);

            // Assert
            Assert.NotNull(result);
            result.Equals(todoModel);
        }

        [Fact]
        public async Task UpdateTodo_WithTodo_Return_TodoModified()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            TodoModel todoModel = TodoMockData.GetSampleTodoModel();

            todoService.Setup(_ => _.CreateTodo(It.IsAny<string>()))
                                    .ReturnsAsync(todoModel);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            TodoUpdateRequest todoRequest = new TodoUpdateRequest("Update Title", false, 10);
            var result = await controller.UpdateTodo(todoModel.Id, todoRequest);

            // Assert
            Assert.NotNull(result);
            result.Equals(todoModel);
        }

        [Fact]
        public async Task DeleteTodo_WithId_Verify()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            TodoModel todoModel = TodoMockData.GetSampleTodoModel();

            todoService.Setup(_ => _.DeleteTodo(It.IsAny<Guid>()))
                                    .ReturnsAsync(1);

            TodoController controller = new TodoController(todoService.Object);

            // Act
            await controller.DeleteTodo(todoModel.Id);

            // Assert
            todoService.Verify(r => r.DeleteTodo(todoModel.Id));
        }

        [Fact]
        public async Task DeleteTodos_WithCompletedEqualNull_Verify()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            IEnumerable<TodoModel> todoModel = TodoMockData.GetSampleTodosModel();

            todoService.Setup(_ => _.DeleteTodos(It.IsAny<bool>()))
                                    .ReturnsAsync(todoModel.Count());

            TodoController controller = new TodoController(todoService.Object);

            // Act
            await controller.DeleteTodos(null);

            // Assert
            todoService.Verify(r => r.DeleteTodos(null));
        }


        [Fact]
        public async Task DeleteTodos_WithCompletedEqualTrue_Verify()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            IEnumerable<TodoModel> todoModel = TodoMockData.GetSampleTodosModel();

            todoService.Setup(_ => _.DeleteTodos(It.IsAny<bool>()))
                                    .ReturnsAsync(todoModel.Count());

            TodoController controller = new TodoController(todoService.Object);

            // Act
            await controller.DeleteTodos(true);

            // Assert
            todoService.Verify(r => r.DeleteTodos(true));
        }

        [Fact]
        public async Task DeleteTodos_WithCompletedEqualFalse_Verify()
        {
            // Arrange
            var todoService = new Mock<ITodoService>();
            IEnumerable<TodoModel> todoModel = TodoMockData.GetSampleTodosModel();

            todoService.Setup(_ => _.DeleteTodos(It.IsAny<bool>()))
                                    .ReturnsAsync(todoModel.Count());

            TodoController controller = new TodoController(todoService.Object);

            // Act
            await controller.DeleteTodos(false);

            // Assert
            todoService.Verify(r => r.DeleteTodos(false));
        }
    }
}