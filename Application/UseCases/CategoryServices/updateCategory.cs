using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryServices;

public class updateCategory(ICategoryRepository repository)
{
    public async Task<Result<Category>> update(int id, CategoryUpdateDTO dto, int userId)
    {
        var category = await repository.GetCategoryAsync(id);
        if (category == null) return Result<Category>.Failure("Categoria nao encontrada", 404);
        if (category.userId != userId) return Result<Category>.Failure("Usuario nao pode alterar categoria de outro usuario", 403);
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<Category>.Failure("Nome da categoria nao pode ser vazio", 400);
        if (await repository.ExistsByNameAndTypeAsync(category.userId, dto.name, dto.type, id)) return Result<Category>.Failure("Categoria ja cadastrada", 409);

        category.name = dto.name.Trim();
        category.type = dto.type;
        await repository.UpdateCategoryAsync(category);
        return Result<Category>.Success(category, 200);
    }
}