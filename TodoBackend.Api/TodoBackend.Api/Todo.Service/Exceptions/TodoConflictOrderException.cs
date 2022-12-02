using System.Runtime.Serialization;

namespace TodoBackend.Api.Todo.Service.Exceptions
{
    public class TodoConflictOrderException : Exception
    {
        public TodoConflictOrderException()
        {
        }

        public TodoConflictOrderException(string? message) : base(message)
        {
        }

        public TodoConflictOrderException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected TodoConflictOrderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
