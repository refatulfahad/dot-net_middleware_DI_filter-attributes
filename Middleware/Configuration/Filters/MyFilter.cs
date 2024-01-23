using Microsoft.AspNetCore.Mvc.Filters;

namespace Middleware.Configuration.Filters
{
    public class MyFilter : IActionFilter
    {
        private readonly string _callerName;

        public MyFilter(string callerName)
        {
            _callerName = callerName;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do something before the action executes.
            Console.WriteLine($"{_callerName} - This filter executed on OnActionExecuting");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.
            Console.WriteLine($"{_callerName} - This filter executed on OnActionExecuted");
        }
    }
}
