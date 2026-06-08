using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GoalRepository(Context context) : IGoalRepository
{
    public Task<Goal?> GetGoalAsync(int goalId) => context.Goals.FindAsync(goalId).AsTask();
    public Task<List<Goal>> GetGoalsAsync(int userId) => context.Goals.Where(g => g.userId == userId).ToListAsync();

    public async Task CreateGoalAsync(Goal goal)
    {
        await context.Goals.AddAsync(goal);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGoalAsync(Goal goal)
    {
        context.Goals.Update(goal);
        await context.SaveChangesAsync();
    }

    public async Task DeleteGoalAsync(Goal goal)
    {
        context.Goals.Remove(goal);
        await context.SaveChangesAsync();
    }
}
