using Domain.Models;

namespace Domain;

public interface ITransactionRepository
{
    Task<Transaction?> GetTransactionAsync(int transactionId);
    Task<List<Transaction>> GetTransactionsByDateAsync(int userId, DateOnly date);
    Task<List<Transaction>> GetTransactionsByTimePeriodAsync(int userId, DateOnly startDate, DateOnly endDate);
    Task CreateTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(Transaction transaction);
}
