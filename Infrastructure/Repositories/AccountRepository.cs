using Domain;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class AccountRepository(Context context) : IAccountRepository
{
    public async Task CreateAccountAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        context.SaveChanges();
        
    }

    public async Task DeleteAccountAsync(int accountId)
    {
        var account = await GetAccount(accountId);
        
        context.Accounts.Remove(account);
        context.SaveChanges();
        
    }

    public async Task UpdateAccountAsync(Account account)
    {
        context.Update(account);
        context.SaveChanges();
        
    }

    public async Task<Account?> GetAccount(int id)
    {
       
        return await context.Accounts.FindAsync(id);
    }
        
}