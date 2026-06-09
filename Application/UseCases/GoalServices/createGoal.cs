using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class createGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> create(GoalCreateDTO dto, int userId)
    {
        if (dto.userId != userId) return Result<Goal>.Failure("Usuario nao pode criar meta para outro usuario", 403);
        if (dto.totalAmount <= 0) return Result<Goal>.Failure("Valor objetivo deve ser maior que zero", 400);
        if (dto.currentAmount < 0) return Result<Goal>.Failure("Valor atual nao pode ser negativo", 400);
        var goal = new Goal(dto.title, dto.totalAmount, dto.currentAmount, dto.userId, dto.deadline);
        await repository.CreateGoalAsync(goal);
        return Result<Goal>.Success(goal, 201);
    }
}