using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Context;
using WorkingWithIdentity.RouteHandler;
using Microsoft.AspNetCore.Identity;
using System.Numerics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankingDbContext>(options =>  
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbContext"))); 

builder.Services.AddScoped(_ => new SqlConnection(builder.Configuration.GetConnectionString("BankingDbContext"))); 

// Adding Authentication and Authorization
builder.Services.AddAuthentication().AddBearerToken();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configura Identity with Entity Framework
builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<BankingDbContext>();


var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment()) 
{ 

    app.UseSwagger(); 
    app.UseSwaggerUI(); 

} 

 

app.UseHttpsRedirection(); 

app.RegisterAccountRoutes();
app.RegisterCustomerRoutes();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
