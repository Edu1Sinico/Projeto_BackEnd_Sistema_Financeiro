using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class createGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> create(GoalCreateDTO dto)
    {
        var goal = new Goal(dto.title, dto.totalAmount, dto.currentAmount, dto.userId);
        await repository.CreateGoalAsync(goal);
        return Result<Goal>.Success(goal, 201);
    }
}