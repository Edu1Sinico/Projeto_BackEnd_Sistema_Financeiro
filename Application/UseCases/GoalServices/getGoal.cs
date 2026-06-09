using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class getGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> getOne(int id, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode acessar meta de outro usuario", 403);
        return Result<Goal>.Success(goal, 200);
    }
}