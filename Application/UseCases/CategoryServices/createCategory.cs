using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryServices;

public class createCategory(ICategoryRepository repository)
{
    public async Task<Result<Category>> create(CategoryCreateDTO dto, int userId)
    {
        if (dto.userId != userId) return Result<Category>.Failure("Usuario nao pode criar categoria para outro usuario", 403);
        if (string.IsNullOrWhiteSpace(dto.name)) return Result<Category>.Failure("Nome da categoria nao pode ser vazio", 400);
        if (await repository.ExistsByNameAndTypeAsync(dto.userId, dto.name, dto.type)) return Result<Category>.Failure("Categoria ja cadastrada", 409);

        var category = new Category(dto.name.Trim(), dto.type, dto.userId);
        await repository.CreateCategoryAsync(category);
        return Result<Category>.Success(category, 201);
    }
}