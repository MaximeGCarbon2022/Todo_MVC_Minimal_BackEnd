using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Todo.Api.Model;

public class TodoCreationRequest
{
    [Required]
    public string Title { get; }

    public TodoCreationRequest(string title)
    {
        Title = title;
    }
}

