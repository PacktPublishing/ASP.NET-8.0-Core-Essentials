using TaskManager;
using TaskManager.Service;
using TaskManager.Service.Contract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR(); // Add signalR service

builder.Services.AddSingleton<ITaskRepository, TaskRepository>(); // Register TaskRepository. This is a singleton service for simulate a database

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

// Add Hub Endpoint
app.MapHub<TaskManagerHub>("/taskManagerHub");

app.Run();
