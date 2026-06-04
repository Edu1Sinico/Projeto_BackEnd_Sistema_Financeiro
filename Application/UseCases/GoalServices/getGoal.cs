using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class getGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> getOne(int id)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) { return Result<Goal>.Failure("Meta não encontrada", 404); }

        return Result<Goal>.Success(goal, 200);
    }
}