using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class deleteGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> delete(int id)
    {

        var goal = await repository.GetGoalAsync(id);
        if (goal == null) { return Result<Goal>.Failure("Meta não encontrada", 404); }

        await  repository.DeleteGoalAsync(goal);
        return Result<Goal>.NoContent();

    }
}