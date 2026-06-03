using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class createUser(IUserRepository repository)
{
    public async Task<Result<User>> create(UserCreateDTO dto)
    {

        var user = new User(dto.name, dto.email, dto.password, DateOnly.FromDateTime(DateTime.UtcNow));
        await repository.CreateUserAsync(user);

        return await Task.FromResult(Result<User>.Success(user, 201));
    }
}