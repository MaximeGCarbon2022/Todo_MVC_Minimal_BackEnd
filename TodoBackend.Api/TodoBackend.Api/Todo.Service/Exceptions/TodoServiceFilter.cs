using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TodoBackend.Api.Todo.Service.Exceptions
{
    public class TodoServiceFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is TodoNotFoundException)
            {
                context.Result = new NotFoundResult();
                context.ExceptionHandled = true;
            }

            if (context.Exception is TodoConflitOrderException)
            {
                context.Result = new ConflictResult();
                context.ExceptionHandled = true;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context) { }
    }
}