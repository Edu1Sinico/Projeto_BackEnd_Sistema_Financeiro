using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class getGoals(IGoalRepository repository)
{
    public async Task<Result<GoalListDTO>> getMany(int userId)
    {
        var goals = new GoalListDTO(await repository.GetGoalsAsync(userId));
        
        return Result<GoalListDTO>.Success(goals, 200);
    }
}