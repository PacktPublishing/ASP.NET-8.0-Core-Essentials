using System.Net;
using System.Text.Json;

namespace CommonMiddlewares.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next; 
  
    public ErrorHandlingMiddleware(RequestDelegate next) 
    { 
        _next = next; 
    } 
  
    public async Task InvokeAsync(HttpContext context) 
    { 
        try 
        { 
            await _next(context); 
        } 
        catch (Exception ex) 
        { 
            await HandleExceptionAsync(context, ex); 
        } 
    } 
  
    private Task HandleExceptionAsync(HttpContext context, Exception exception) 
    { 
        context.Response.ContentType = "application/json"; 
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; 
        return context.Response.WriteAsync(new ErrorDetails() 
        { 
            StatusCode = context.Response.StatusCode, 
            Message = "Internal Server Error from the custom middleware." 
        }.ToString()); 
    } 
}

public class ErrorDetails 
{ 
    public int StatusCode { get; set; } 
    public string Message { get; set; } 
  
    public override string ToString() 
    { 
        return JsonSerializer.Serialize(this); 
    } 
} 