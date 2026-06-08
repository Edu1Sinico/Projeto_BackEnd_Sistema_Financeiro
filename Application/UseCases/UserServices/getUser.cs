using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class getUser(IUserRepository repository)
{
    public async Task<Result<User>> getOne(int id)
    {
        var user = await repository.GetUserAsync(id);
        return user == null ? Result<User>.Failure("Usuario nao encontrado", 404) : Result<User>.Success(user, 200);
    }
}
