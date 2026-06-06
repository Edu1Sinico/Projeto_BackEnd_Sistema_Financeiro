using Domain.Models;

namespace Domain;

public interface IUserRepository
{
    Task <User?> AuthenticateUser(string email, string password);
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(User user);
    Task<User?> GetUserAsync(int userId);
    Task UpdateUserAsync(User user);

    

}

