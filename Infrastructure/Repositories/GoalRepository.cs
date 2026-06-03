using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GoalRepository(Context context) : IGoalRepository
{
    public async Task CreateGoalAsync(Goal goal)
    {
        context.Goals.Add(goal);
        context.SaveChanges();
        
    }

    public async Task DeleteGoalAsync(int goalId)
    {

        var goal = await GetGoalAsync(goalId);

        context.Goals.Remove(goal);
        context.SaveChanges();
        
    }

    public async Task<Goal?> GetGoalAsync(int goalId)
    {
        
        return await context.Goals.FindAsync(goalId);
    }

    public async Task<List<Goal>> GetGoalsAsync(int userId)
    {
        return await context.Goals.Where(g => g.userId == userId).ToListAsync();
    }
    

    public Task UpdateGoalAsync(Goal goal)
    {
        context.SaveChanges();
        return Task.CompletedTask;
    }
}