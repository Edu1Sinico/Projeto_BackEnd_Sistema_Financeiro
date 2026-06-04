using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository(Context context) : ITransactionRepository
{
    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
       
    }

    public async Task<Transaction?> GetTransactionAsync(int transactionId)
    {
        if (!context.Transactions.Any(t => t.id == transactionId))
        {
            return null;
        }

        return await context.Transactions.FindAsync(transactionId);
    }

    public async Task<List<Transaction>> GetTransactionsByDateAsync(int userId, DateOnly date)
    {


        return await context.Accounts.Where(a => a.userId == userId)
            .SelectMany(a => a.transactions
                .Where(t => t.transactionDate == date)).ToListAsync();
                
    }

    public async Task<List<Transaction>> GetTransactionsByTimePeriodAsync(int userId, DateOnly startDate,
        DateOnly endDate)
    {
        return await context.Accounts.Where(a=> a.userId == userId)
            .SelectMany(a => a.transactions
                .Where(t => t.transactionDate >= startDate && t.transactionDate <=endDate)).ToListAsync();
    }
}