using DynamicConfiguration.Models;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);



// Add Azure App Configuration
builder.Services.AddAzureAppConfiguration();
builder.Services.AddFeatureManagement();

var connectionString = builder.Configuration.GetConnectionString("AppConfig");
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(connectionString)
        .Select("DynamicConfiguration:*", LabelFilter.Null)
        .ConfigureRefresh(refreshOptions =>
            refreshOptions.Register("DynamicConfiguration:Sentinel", refreshAll: true))
        .UseFeatureFlags(featureFlagsOptions =>
        {
            featureFlagsOptions.CacheExpirationInterval = TimeSpan.FromSeconds(5);
        });
});

// Bind configuration to settings classes
builder.Services.Configure<PaymentSettings>(builder.Configuration.GetSection("PaymentSettings"));
builder.Services.Configure<ShippingSettings>(builder.Configuration.GetSection("ShippingSettings"));

// Add MVC support
builder.Services.AddControllersWithViews();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware for refreshing configuration
app.UseAzureAppConfiguration();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
