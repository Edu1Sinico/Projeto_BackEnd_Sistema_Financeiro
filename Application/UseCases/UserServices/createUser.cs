using Application.DTOs;
using Application.Interfaces;
using Domain;
using Domain.Models;

namespace Application.UseCases.UserServices;

public class createUser(IUserRepository repository, IHashingService hashingService)
{
    public async Task<Result<User>> create(UserCreateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<User>.Failure("Nome nao pode ser vazio", 400);
        if (await repository.GetUserByEmailAsync(dto.email) != null) return Result<User>.Failure("Email ja cadastrado", 409);

        var user = new User(dto.name.Trim(), dto.email, hashingService.HashText(dto.password), DateOnly.FromDateTime(DateTime.UtcNow));
        await repository.CreateUserAsync(user);
        return Result<User>.Success(user, 201);
    }
}
