using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class deleteUser(IUserRepository repository)
{
    public async Task<Result<User>> delete(int id)
    {
        await repository.DeleteUserAsync(id);
        return Result<User>.NoContent();
    }
}