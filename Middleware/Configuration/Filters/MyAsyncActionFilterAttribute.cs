using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware.Configuration.Filters
{
    public class MyAsyncActionFilterAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        private readonly string _callerName;
        public int Order { get; set; }
        public MyAsyncActionFilterAttribute(string callerName, int order = 0)
        {
            _callerName = callerName;
            Order = order;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.First().Value is string stringValue &&  !stringValue.Contains("weather"))
            {
                context.Result = new ContentResult
                {
                    Content = "Please use 'weather' keyword",
                    StatusCode = 200, // Set the appropriate status code
                    ContentType = "text/plain" // Set the appropriate content type
                };

                return;
            }
            Console.WriteLine($"{_callerName} - This filter executed on OnActionExecuting");

            await next();

            Console.WriteLine($"{_callerName} - This filter executed on OnActionExecuted");
        }
    }
}
