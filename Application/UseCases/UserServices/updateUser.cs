using Application.DTOs;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.UserServices;

public class updateUser(IUserRepository repository)
{
    public async Task<Result<User>> update(int id, UserUpdateDTO dto)
    {
        var user = await repository.GetUserAsync(id);
        if (user == null) return Result<User>.Failure("Usuario nao encontrado", 404);
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<User>.Failure("Nome nao pode ser vazio", 400);

        var existing = await repository.GetUserByEmailAsync(dto.email);
        if (existing != null && existing.id != id) return Result<User>.Failure("Email ja cadastrado", 409);

        user.name = dto.name.Trim();
        user.email = dto.email;
        await repository.UpdateUserAsync(user);
        return Result<User>.Success(user, 200);
    }
}