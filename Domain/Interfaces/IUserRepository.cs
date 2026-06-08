using Domain.Models;

namespace Domain;

public interface IUserRepository
{
    Task<User?> AuthenticateUser(string email, string password);
    Task<User?> GetUserAsync(int userId);
    Task<User?> GetUserByEmailAsync(string email);
    Task CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
