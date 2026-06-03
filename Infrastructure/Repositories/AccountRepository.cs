using Domain;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class AccountRepository(Context context) : IAccountRepository
{
    public Task CreateAccountAsync(Account account)
    {
        context.Accounts.AddAsync(account);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteAccountAsync(int accountId)
    {
        var account = GetAccount(accountId).Result;
        
        context.Accounts.Remove(account);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task UpdateAccountAsync(Account account)
    {
        context.Update(account);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task<Account?> GetAccount(int id)
    {
       
        return await context.Accounts.FindAsync(id);
    }
        
}