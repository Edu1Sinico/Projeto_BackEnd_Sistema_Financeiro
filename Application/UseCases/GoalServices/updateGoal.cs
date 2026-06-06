using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class updateGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> update(int id, GoalUpdateDTO dto)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) { return Result<Goal>.Failure("Meta não encontrada", 404); }

        goal.title = dto.title;
        goal.totalAmount = dto.totalAmount;
        goal.currentAmount = dto.currentAmount;

        await repository.UpdateGoalAsync(goal);
        return Result<Goal>.Success(goal, 200);
    }
}