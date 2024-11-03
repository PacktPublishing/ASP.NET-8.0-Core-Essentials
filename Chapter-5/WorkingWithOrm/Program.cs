using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WorkingWithOrm.Context;
using WorkingWithOrm.RouteHandler;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BankingDbContext>(options =>  
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankingDbContext"))); 

builder.Services.AddScoped(_ => new SqlConnection(builder.Configuration.GetConnectionString("BankingDbContext"))); 

var app = builder.Build();


app.MapAccountHandler();
app.MapCustomerHandler();

app.Run();
