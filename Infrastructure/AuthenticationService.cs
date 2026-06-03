using Application.DTOs;
using Infrastructure.Persistence;

namespace Infrastructure;

public class AuthenticationService(Context context, HashingServices hashing)
{
    public Result<string> validate(UserCredentialsDTO userCredentialsDto)
    {
        var encodedPassword = hashing.hashText(userCredentialsDto.password);
        
        if (!context.Users.Where(u => u.email == userCredentialsDto.email && u.password == encodedPassword))
        {
            return Result<string>.failure("Usuario não encontrado", 404);
        }
    }
}