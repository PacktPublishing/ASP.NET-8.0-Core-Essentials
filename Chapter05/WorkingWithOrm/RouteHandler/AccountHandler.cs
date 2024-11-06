using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithOrm.Context;
using WorkingWithOrm.Model;

namespace WorkingWithOrm.RouteHandler;

public static class AccountHandler
{
    public static void MapAccountHandler(this WebApplication app)
    {
        app.MapGet("/account", async (CancellationToken cancellationToken, BankingDbContext dbContext) => 

        { 

            var accountd = await dbContext.Accounts.ToListAsync(cancellationToken); 

            return Results.Ok(accountd); 

        }); 

 

        app.MapGet("/account/{id}", async (int id, BankingDbContext dbContext, CancellationToken cancellationToken) => 

        { 

            var account = await dbContext.Accounts.FindAsync(id, cancellationToken); 

            return Results.Ok(account); 

        }); 

 

        app.MapPost("/account", async ([FromBody]Account account, BankingDbContext dbContext, CancellationToken cancellationToken) => 

        { 

            await dbContext.Accounts.AddAsync(account, cancellationToken); 

            await dbContext.SaveChangesAsync(cancellationToken); 

            return Results.Created(); 

        }); 
    }
}