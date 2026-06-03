using Domain.Models;

namespace Domain;

public interface ITransactionRepository
{
    Task CreateTransactionAsync(Transaction transaction);
    Task<Transaction?> GetTransactionAsync(int transactionId);
    Task<List<Transaction>> GetTransactionsByDateAsync(int userId, DateOnly date);
    Task<List<Transaction>> GetTransactionsByTimePeriodAsync(int userId, DateOnly startDate,
        DateOnly endDate);

}