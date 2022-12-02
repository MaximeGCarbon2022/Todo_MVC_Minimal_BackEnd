namespace TodoBackend.Api.Todo.Api.Model;

public record TodoResponse(
    Guid Id,
    string Title,
    bool Completed,
    int Order,
    string Url
);

