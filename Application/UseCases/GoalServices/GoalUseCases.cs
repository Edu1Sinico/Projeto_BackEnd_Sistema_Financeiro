using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.GoalServices;

public class createGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> create(GoalCreateDTO dto, int userId)
    {
        if (dto.userId != userId) return Result<Goal>.Failure("Usuario nao pode criar meta para outro usuario", 403);
        if (dto.totalAmount <= 0) return Result<Goal>.Failure("Valor objetivo deve ser maior que zero", 400);
        if (dto.currentAmount < 0) return Result<Goal>.Failure("Valor atual nao pode ser negativo", 400);
        var goal = new Goal(dto.title, dto.totalAmount, dto.currentAmount, dto.userId, dto.deadline);
        await repository.CreateGoalAsync(goal);
        return Result<Goal>.Success(goal, 201);
    }
}

public class getGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> getOne(int id, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode acessar meta de outro usuario", 403);
        return Result<Goal>.Success(goal, 200);
    }
}

public class getGoals(IGoalRepository repository)
{
    public async Task<Result<List<Goal>>> getMany(int userId, int authenticatedUserId)
    {
        if (userId != authenticatedUserId) return Result<List<Goal>>.Failure("Usuario nao pode listar metas de outro usuario", 403);
        return Result<List<Goal>>.Success(await repository.GetGoalsAsync(userId), 200);
    }
}

public class updateGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> update(int id, GoalUpdateDTO dto, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode alterar meta de outro usuario", 403);
        if (dto.totalAmount <= 0) return Result<Goal>.Failure("Valor objetivo deve ser maior que zero", 400);
        if (dto.currentAmount < 0) return Result<Goal>.Failure("Valor atual nao pode ser negativo", 400);
        goal.title = dto.title;
        goal.totalAmount = dto.totalAmount;
        goal.currentAmount = dto.currentAmount;
        goal.deadline = dto.deadline;
        goal.completed = dto.currentAmount >= dto.totalAmount;
        await repository.UpdateGoalAsync(goal);
        return Result<Goal>.Success(goal, 200);
    }
}

public class deleteGoal(IGoalRepository repository)
{
    public async Task<Result<Goal>> delete(int id, int userId)
    {
        var goal = await repository.GetGoalAsync(id);
        if (goal == null) return Result<Goal>.Failure("Meta nao encontrada", 404);
        if (goal.userId != userId) return Result<Goal>.Failure("Usuario nao pode excluir meta de outro usuario", 403);
        await repository.DeleteGoalAsync(goal);
        return Result<Goal>.NoContent();
    }
}
