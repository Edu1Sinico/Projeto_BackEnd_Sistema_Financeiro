using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository(Context context) : ITransactionRepository
{
    public Task<Transaction?> GetTransactionAsync(int transactionId) => context.Transactions.FindAsync(transactionId).AsTask();
    
    public Task<List<Transaction>> GetTransactionsByDateAsync(int userId, DateOnly date, int page, int quantity) =>
        context.Transactions.Where(t => t.account.userId == userId && t.transactionDate == date)
            .OrderByDescending(t => t.id)
            .Skip((page - 1)*quantity).Take(quantity).ToListAsync();
    
    public Task<List<Transaction>> GetTransactionsByTimePeriodAsync(int userId, DateOnly startDate, DateOnly endDate, int page, int quantity) =>
        context.Transactions.Where(t => t.account.userId == userId && t.transactionDate >= startDate && t.transactionDate <= endDate)
            .OrderByDescending(t => t.transactionDate)
            .ThenByDescending(t => t.id)
            .Skip((page - 1)*quantity).Take(quantity).ToListAsync();

    public async Task CreateTransactionAsync(Transaction transaction)
    {
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
    }

    public async Task UpdateTransactionAsync(Transaction transaction)
    {
        context.Transactions.Update(transaction);
        await context.SaveChangesAsync();
    }

    public async Task DeleteTransactionAsync(Transaction transaction)
    {
        context.Transactions.Remove(transaction);
        await context.SaveChangesAsync();
    }

    
}