using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Api.Todo.Api.Model;

public class TodoUpdateRequest
{
    [Required]
    public string Title { get; }

    [Required]
    public bool Completed { get; }

    [Required]
    public int Order { get; }

    public TodoUpdateRequest(string title, bool completed, int order)
    {
        Title = title;
        Completed = completed;
        Order = order;
    }
}

