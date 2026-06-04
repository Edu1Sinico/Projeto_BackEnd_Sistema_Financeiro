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
        await context.SaveChangesAsync();
        
    }

    public async Task DeleteGoalAsync(Goal goal)
    {

        context.Goals.Remove(goal);
        await context.SaveChangesAsync();
        
    }

    public async Task<Goal?> GetGoalAsync(int goalId)
    {
        
        return await context.Goals.FindAsync(goalId);
    }

    public async Task<List<Goal>> GetGoalsAsync(int userId)
    {
        return await context.Goals.Where(g => g.userId == userId).ToListAsync();
    }
    

    public async Task UpdateGoalAsync(Goal goal)
    {
        await context.SaveChangesAsync();
        
    }
}