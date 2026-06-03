using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class getUser(IUserRepository repository)
{
    public async Task<Result<User>> getOne(int id)
    {
        var user =  await repository.GetUserAsync(id);
        if (user == null)
        {
            return Result<User>.Failure("Usuario não encontrado", 404);
        }
        
        return Result<User>.Success(user, 200);
    }
}