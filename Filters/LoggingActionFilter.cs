using AspNetCoreLoggingAPI.Data;
using AspNetCoreLoggingAPI.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AspNetCoreLoggingAPI.Filters
{
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _context;

        public LoggingActionFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Capture request information: path and method.
            var requestPath = context.HttpContext.Request.Path;
            var requestMethod = context.HttpContext.Request.Method;
            var requestInfo = $"Path: {requestPath}; Method: {requestMethod}";

            // Execute the action (call the next step in the pipeline).
            var executedContext = await next();

            // Capture response information: status code.
            var responseStatusCode = context.HttpContext.Response.StatusCode;
            var responseInfo = $"Status Code: {responseStatusCode}";

            // Create a log record
            var logRecord = new LogRecord
            {
                Request = requestInfo,
                Response = responseInfo,
                Timestamp = DateTime.UtcNow
            };

            _context.LogRecords.Add(logRecord);
            await _context.SaveChangesAsync();
        }
    }
}
