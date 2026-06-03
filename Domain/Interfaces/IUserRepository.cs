using Domain.Models;

namespace Domain;

public interface IUserRepository
{
    Task <bool> AuthenticateUser(string email, string password);
    Task CreateUserAsync(User user);
    Task DeleteUserAsync(int userId);
    Task<User?> GetUserAsync(int userId);
    Task UpdateUserAsync(User user);

    

}

