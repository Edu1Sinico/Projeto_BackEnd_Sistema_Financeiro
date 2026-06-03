using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository(Context context) : ITransactionRepository
{
    public Task CreateTransactionAsync(Transaction transaction)
    {
        context.Transactions.Add(transaction);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task<Transaction?> GetTransactionAsync(int transactionId)
    {
        if (!context.Transactions.Any(t => t.id == transactionId))
        {
            return null;
        }

        return Task.FromResult(context.Transactions.Find(transactionId));
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