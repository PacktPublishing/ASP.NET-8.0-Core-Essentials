/*
    This example demonstrates how to add custom middleware to an ASP.NET Core application.
*/

var builder = WebApplication.CreateBuilder(args); 

var app = builder.Build(); 
  

app.UseMiddleware<ErrorHandlingMiddleware>(); 
app.UseMiddleware<AuthenticationMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>(); 
 
app.Run(async context => 
{ 
    await context.Response.WriteAsync("Hello, World!"); 
}); 

app.Run(); 