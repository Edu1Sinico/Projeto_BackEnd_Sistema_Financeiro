using Domain.Models;

namespace Domain;

public interface IAccountRepository
{
    Task<Account?> GetAccount(int id);
    Task<List<Account>> GetAccountsByUserAsync(int userId, int page, int quantity);
    Task CreateAccountAsync(Account account);
    Task UpdateAccountAsync(Account account);
    Task DeleteAccountAsync(Account account);
}
