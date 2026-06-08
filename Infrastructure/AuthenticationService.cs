using Domain;
using Domain.Models;

namespace Infrastructure;

public class AuthenticationService(IUserRepository repository, HashingServices hashing, TokenService tokenService)
{
    public async Task<Result<string>> validate(UserCredentialsDTO dto)
    {
        var user = await repository.AuthenticateUser(dto.email, hashing.HashText(dto.password));
        if (user == null) return Result<string>.Failure("Usuario ou senha incorretos", 404);
        return Result<string>.Success(tokenService.generateToken(user).value!, 200);
    }
}
