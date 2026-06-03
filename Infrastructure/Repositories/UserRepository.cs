using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(Context context) : IUserRepository
{
    public Task<bool> AuthenticateUser(string email, string password)
    {
        if (!context.Users.Any(u => u.email == email && u.password == password))
        {
            return Task.FromResult(false);
        }
        
        return Task.FromResult(true);
    }

    public Task CreateUserAsync(User user)
    {
        context.Users.AddAsync(user);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task DeleteUserAsync(int userId)
    {
        var user = GetUserAsync(userId).Result;
        
        
        context.Users.Remove(user);
        context.SaveChanges();
        return Task.CompletedTask;
    }

    public async Task<User?> GetUserAsync(int userId)
    {
        return await context.Users.FindAsync(userId);
    }

    public Task UpdateUserAsync(User user)
    {
        context.Users.Update(user);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}