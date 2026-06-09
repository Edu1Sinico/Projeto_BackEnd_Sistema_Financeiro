using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository(Context context) : IAccountRepository
{
    public Task<Account?> GetAccount(int id) => context.Accounts.FindAsync(id).AsTask();
    public Task<List<Account>> GetAccountsByUserAsync(int userId, int page, int quantity) => context.Accounts.Where(a => a.userId == userId)
        .Skip((page - 1)*quantity).Take(quantity).ToListAsync();

    public async Task CreateAccountAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAccountAsync(Account account)
    {
        context.Accounts.Update(account);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAccountAsync(Account account)
    {
        context.Accounts.Remove(account);
        await context.SaveChangesAsync();
    }
}
