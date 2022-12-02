namespace TodoBackend.Api.Todo.Service;

public record TodoModel(
    Guid Id,
    string Title,
    bool Completed,
    int Order
);