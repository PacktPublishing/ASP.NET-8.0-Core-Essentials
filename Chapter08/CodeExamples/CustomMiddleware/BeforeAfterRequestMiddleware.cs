/*
   An example of a custom middleware that logs request and response information.
*/

public class BeforeAfterRequestMiddleware
{
    private readonly RequestDelegate _next;

    public BeforeAfterRequestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Logging request information 
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline 
        await _next(context);

        // Logging response information 
        Console.WriteLine($"Response: {context.Response.StatusCode}");
    }
}