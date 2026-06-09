using Domain.Models;

namespace Domain;

public interface ITransactionRepository
{
    Task<Transaction?> GetTransactionAsync(int transactionId);
    Task<List<Transaction>> GetTransactionsByDateAsync(int userId, DateOnly date, int page, int quantity);
    Task<List<Transaction>> GetTransactionsByTimePeriodAsync(int userId, DateOnly startDate, DateOnly endDate, int page, int quantity);
    Task CreateTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(Transaction transaction);
    Task DeleteTransactionAsync(Transaction transaction);
}
