using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Context;
using WorkingWithIdentity.Model;

namespace WorkingWithIdentity.RouteHandler;

public static class AccountHandler
{
    public static void RegisterAccountRoutes(this WebApplication app)
    {
        app.MapGet("/account", async (CancellationToken cancellationToken, BankingDbContext dbContext) => 

        { 

            var accountd = await dbContext.Accounts.ToListAsync(cancellationToken); 

            return Results.Ok(accountd); 

        }).RequireAuthorization(); 

 

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