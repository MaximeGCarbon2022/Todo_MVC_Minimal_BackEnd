using System.Runtime.Serialization;

namespace TodoBackend.Api.Todo.Service.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException()
    {
    }

    public TodoNotFoundException(string? message) : base(message)
    {
    }

    public TodoNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected TodoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}

