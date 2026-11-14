namespace WebAPI.Middlewares;

public class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLoggingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("Request: {Path} at {Time}", context.Request.Path, DateTime.Now);
        
        await _next(context);
        
        _logger.LogInformation("Response: {StatusCode} at {Time}", context.Response.StatusCode, DateTime.Now);
    }
}