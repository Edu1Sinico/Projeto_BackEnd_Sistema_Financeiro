using Application.DTOs;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Identity;


public class updateUser(IUserRepository repository)
{
    public async Task<Result<User>> update(int id, UserUpdateDTO dto)
    {
        var user = await repository.GetUserAsync(id);
        if (user == null)
        {
            return Result<User>.Failure("Usuario não encontrado", 404);
        }
        
        user.name = dto.name;
        user.email = dto.email;
        
        await repository.UpdateUserAsync(user);
        return Result<User>.Success(user, 200);
    }
}