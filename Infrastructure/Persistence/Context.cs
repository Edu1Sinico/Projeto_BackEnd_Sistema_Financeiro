using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Context : DbContext
{
    
    public Context(DbContextOptions<Context> options) : base(options){}

    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionGoal> TransactionGoals { get; set; }
    public DbSet<Goal> Goals { get; set; }

}