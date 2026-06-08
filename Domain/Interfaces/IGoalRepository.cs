using Domain.Models;

namespace Domain;

public interface IGoalRepository
{
    Task<Goal?> GetGoalAsync(int goalId);
    Task<List<Goal>> GetGoalsAsync(int userId);
    Task CreateGoalAsync(Goal goal);
    Task UpdateGoalAsync(Goal goal);
    Task DeleteGoalAsync(Goal goal);
}
