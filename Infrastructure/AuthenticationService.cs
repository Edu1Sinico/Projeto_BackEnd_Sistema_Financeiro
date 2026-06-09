using Domain;
using Domain.Models;
using Infrastructure.Persistence;

namespace Infrastructure;

public class AuthenticationService(IUserRepository repository, HashingServices hashing, TokenService tokenService)
{
    public async Task<Result<string>> validate(UserCredentialsDTO userCredentialsDto)
    {
        var encodedPassword = hashing.hashText(userCredentialsDto.password);

        var user = await repository.AuthenticateUser(userCredentialsDto.email, encodedPassword);

        if(user == null){    return Result<string>.Failure("Usuario ou senhas incorretos", 404);
        }

        var token = tokenService.generateToken(user);
        return Result<string>.Success(token.value,200);
    }
}