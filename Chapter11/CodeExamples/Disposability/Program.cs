using Microsoft.AspNetCore.Builder; 
using Microsoft.Extensions.Hosting; 
using Microsoft.Extensions.Logging; 
  
var builder = WebApplication.CreateBuilder(args); 
builder.Services.AddControllers();  
var app = builder.Build();  
app.UseRouting(); 
app.UseEndpoints(endpoints => 
{ 
    endpoints.MapControllers(); 
}); 
  
var host = app.Services.GetService<IHostApplicationLifetime>();  

// Handle graceful shutdown 
host.ApplicationStopping.Register(() => 
{ 
    var logger = app.Services.GetService<ILogger<Program>>(); 
    logger.LogInformation("Application is shutting down...");     
    // Add more cleanup tasks here 
}); 
  

app.Run(); 