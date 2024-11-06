using System.Diagnostics;

namespace CommonMiddlewares.Middlewares;

public class PerformanceLoggingMiddleware
{
    private readonly RequestDelegate _next; 
  
    public PerformanceLoggingMiddleware(RequestDelegate next) 
    { 
        _next = next; 
    } 
  
    public async Task InvokeAsync(HttpContext context, ILogger<PerformanceLoggingMiddleware> logger) 
    { 
        var timestamp = Stopwatch.GetTimestamp(); 
        await _next(context);  
        var elapsedMilliseconds = Stopwatch.GetElapsedTime(timestamp).TotalMilliseconds; 
  
        logger.LogInformation("Request {Method} {Path} took {ElapsedMilliseconds} ms", 
            context.Request.Method, context.Request.Path, elapsedMilliseconds); 
    } 
}