using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WorkingWithOrm.Context;
using WorkingWithOrm.Model;

namespace WorkingWithOrm.RouteHandler;

public static class CustomerHandler
{
    public static void MapCustomerHandler(this WebApplication app)
    {
        app.MapGet("/customers", async (CancellationToken cancellationToken, BankingDbContext dbContext) => 

        { 

            var customers = await dbContext.Customers.ToListAsync(cancellationToken); 

            return Results.Ok(customers); 

        }); 

 

        app.MapGet("/customers/{id}", async (int id, BankingDbContext dbContext, CancellationToken cancellationToken) => 

        { 

            var customer = await dbContext.Customers.FindAsync(id, cancellationToken); 

            return Results.Ok(customer); 

        }); 

 

        app.MapPost("/customers", async ([FromBody]Customer customer, BankingDbContext dbContext, CancellationToken cancellationToken) => 

        { 

            await dbContext.Customers.AddAsync(customer, cancellationToken); 

            await dbContext.SaveChangesAsync(cancellationToken); 

            return Results.Created(); 

        }); 
        
        app.MapGet("GetAllCustomersUsingDapper", async(SqlConnection connection) => 

        { 

            var customers = await connection.QueryAsync<Customer>("SELECT Id, Name FROM Customers ORDER BY Name"); 

 

            return Results.Ok(customers); 

        }); 
        
        app.MapGet("GetCustomerByIdUsingDapper", async(int id, SqlConnection connection) => 

        { 

            var customer = await connection.QueryFirstOrDefaultAsync<Customer>("SELECT Id, Name FROM Customers WHERE Id = @Id", new { Id = id }); 



            if (customer is null) return Results.NotFound(); 

 

            return Results.Ok(customer); 

        }); 
    }
}