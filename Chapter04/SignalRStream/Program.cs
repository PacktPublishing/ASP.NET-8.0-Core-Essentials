using SignalRStreamingApp.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Add SignalR
builder.Services.AddSignalR();
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
app.MapHub<StreamHub>("/streamHub");

app.Run();
