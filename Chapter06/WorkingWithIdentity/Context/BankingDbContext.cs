using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkingWithIdentity.Model;

namespace WorkingWithIdentity.Context;

public class BankingDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Customer> Customers { get; set; } 

    public DbSet<Account> Accounts { get; set; } 

    public DbSet<Movement> Movements { get; set; }
    
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
        
    }
}