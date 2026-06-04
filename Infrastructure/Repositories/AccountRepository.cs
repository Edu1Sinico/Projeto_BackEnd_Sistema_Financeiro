using Domain;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class AccountRepository(Context context) : IAccountRepository
{
    public async Task CreateAccountAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();
        
    }

    public async Task DeleteAccountAsync(Account account)
    {
       
        context.Accounts.Remove(account);
        await context.SaveChangesAsync();
        
    }

    public async Task UpdateAccountAsync(Account account)
    {
        context.Update(account);
        await context.SaveChangesAsync();
        
    }

    public async Task<Account?> GetAccount(int id)
    {
       
        return await context.Accounts.FindAsync(id);
    }
        
}