/*
    This is a middlware implmementation using a Factory pattern.
    The middleware implements the IMiddleware interface and overrides the InvokeAsync method.
    The InvokeAsync method contains the logic to limit the number of requests.
*/

public class RequestLimitingMiddleware : IMiddleware
{
    private readonly ILogger<RequestLimitingMiddleware> _logger;

    public RequestLimitingMiddleware(ILogger<RequestLimitingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Logic to limit the number of requests 
        _logger.LogInformation("Processing request");
        await next(context);
    }
}