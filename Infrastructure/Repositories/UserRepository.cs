using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Context context) : IUserRepository
{
    public Task<User?> AuthenticateUser(string email, string password) => context.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);
    public Task<User?> GetUserAsync(int userId) => context.Users.FindAsync(userId).AsTask();
    public Task<User?> GetUserByEmailAsync(string email) => context.Users.FirstOrDefaultAsync(u => u.email == email);

    public async Task CreateUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
    }
}
