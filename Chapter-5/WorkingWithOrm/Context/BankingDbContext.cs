using Microsoft.EntityFrameworkCore;
using WorkingWithOrm.Model;

namespace WorkingWithOrm.Context;

public class BankingDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } 

    public DbSet<Account> Accounts { get; set; } 

    public DbSet<Movement> Movements { get; set; }
    
    public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
    {
        
    }
}