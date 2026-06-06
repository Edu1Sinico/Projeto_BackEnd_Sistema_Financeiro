using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Context context) : IUserRepository
{
    public async Task<User?> AuthenticateUser(string email, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);

        return user;
    }

    public async Task CreateUserAsync(User user)
    {
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        
    }

    public async Task DeleteUserAsync(User user)
    {
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        return await context.Users.FindAsync(userId);
    }

    public async Task UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
        
    }
}