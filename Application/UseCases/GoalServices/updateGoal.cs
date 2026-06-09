using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class updateGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> update(int id, GoalUpdateDTO dto, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode alterar meta de outro usuario", 403);
        if (dto.totalAmount <= 0) return Result<Goal>.Failure("Valor objetivo deve ser maior que zero", 400);
        if (dto.currentAmount < 0) return Result<Goal>.Failure("Valor atual nao pode ser negativo", 400);
        goal.title = dto.title;
        goal.totalAmount = dto.totalAmount;
        goal.currentAmount = dto.currentAmount;
        goal.deadline = dto.deadline;
        goal.completed = dto.currentAmount >= dto.totalAmount;
        await repository.UpdateGoalAsync(goal);
        return Result<Goal>.Success(goal, 200);
    }
}