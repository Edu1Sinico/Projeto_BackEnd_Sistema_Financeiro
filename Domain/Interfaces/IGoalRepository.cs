using Domain.Models;

namespace Domain;

public interface IGoalRepository
{
    Task CreateGoalAsync(Goal goal);
    Task DeleteGoalAsync(Goal goal);
    Task<Goal?> GetGoalAsync(int goalId);
    Task<List<Goal>> GetGoalsAsync(int userId);
    Task UpdateGoalAsync(Goal goal);

}