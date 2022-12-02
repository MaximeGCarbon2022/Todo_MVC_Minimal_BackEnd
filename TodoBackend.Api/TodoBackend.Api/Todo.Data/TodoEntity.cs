namespace TodoBackend.Api.Todo.Data;

public record TodoEntity(
    Guid Id,
    string Title,
    bool Completed,
    int Order
);
