
/*
    This middleware checks for the presence of an API key in the request header.
*/
public class ApiKeyCheckMiddleware 
{ 
    private readonly RequestDelegate _next; 
    private const string API_KEY = "X-API-KEY"; 
    private const string VALID_API_KEY = "XYZ123"; 

    public ApiKeyCheckMiddleware (RequestDelegate next) 
    { 
        _next = next; 
    } 
  
    public async Task InvokeAsync(HttpContext context) 
    { 
        if (!context.Request.Headers.TryGetValue(API_KEY, out var extractedApiKey) || extractedApiKey != VALID_API_KEY) 
        { 
            context.Response.StatusCode = 401; 
            await context.Response.WriteAsync("Unauthorized"); 
            return; 
        } 
        await _next(context); 
    } 
} 