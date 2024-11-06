/*
    Example of Program.cs using a Factory pattern to create middleware
*/

var builder = WebApplication.CreateBuilder(args); 

// Add Middleware to the DI container
builder.Services.AddScoped<RequestLimitingMiddleware>(); 
  
var app = builder.Build(); 

// Adding custom middleware
app.UseMiddleware<RequestLimitingMiddleware>(); 

app.Run(async context => 
{ 
    await context.Response.WriteAsync("Hello, World!"); 
}); 

app.Run(); 