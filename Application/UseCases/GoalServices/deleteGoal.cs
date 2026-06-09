using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class deleteGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> delete(int id, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode excluir meta de outro usuario", 403);
        await repository.DeleteGoalAsync(goal);
        return Result<Goal>.NoContent();
    }
}