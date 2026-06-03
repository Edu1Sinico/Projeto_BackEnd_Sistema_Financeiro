using Domain.Models;

namespace Domain;

public interface IAccountRepository
{
    Task CreateAccountAsync(Account account);
    Task DeleteAccountAsync(int accountId);
    Task UpdateAccountAsync(Account account);
    Task<Account?> GetAccount(int id);
}