var builder = WebApplication.CreateBuilder(args); 
var app = builder.Build();   

// Adding custom middleware
app.UseMiddleware<BeforeAfterRequestMiddleware>(); 
app.Run(async context => 
{ 
    await context.Response.WriteAsync("Hello, World!"); 
}); 


app.Run(); 