using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class deleteUser(IUserRepository repository)
{
    public async Task<Result<User>> delete(int id)
    {
        var user = await repository.GetUserAsync(id);
        if (user == null) return Result<User>.Failure("Usuario nao encontrado", 404);
        await repository.DeleteUserAsync(user);
        return Result<User>.NoContent();
    }
}
