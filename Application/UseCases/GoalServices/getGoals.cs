using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class getGoals(IGoalRepository repository)
{
    public async Task<Result<GoalListDTO>> getMany(int userId, int authenticatedUserId,int page, int quantity)
    {
        if (userId != authenticatedUserId) return Result<GoalListDTO>.Failure("Usuario nao pode listar metas de outro usuario", 403);
        return Result<GoalListDTO>.Success(new GoalListDTO(await repository.GetGoalsAsync(userId, page,  quantity),page,quantity), 200);
    }
}