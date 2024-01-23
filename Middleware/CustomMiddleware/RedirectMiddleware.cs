namespace Middleware.CustomMiddleware
{
    public class RedirectMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestDelegate> _logger;
        public RedirectMiddleware(ILogger<RequestDelegate> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.Contains("1") && !context.Request.Path.Value.Contains("SearchInFo"))
            {
                context.Response.Redirect(context.Request.Path.Value + "/1");
                return;
            }

            await _next.Invoke(context);

            _logger.LogWarning("After getting response.");
        }
    }
}
