using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Context : DbContext
{
    
    public Context(DbContextOptions<Context> options) : base(options){}

    DbSet<User> Users { get; set; }
    DbSet<Account> Accounts { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<Goal> Goals { get; set; }

}