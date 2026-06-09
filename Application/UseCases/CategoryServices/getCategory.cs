using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryServices;

public class getCategory(ICategoryRepository repository)
{
    public async Task<Result<Category>> getOne(int id, int userId)
    {
        var category = await repository.GetCategoryAsync(id);
        if (category == null) return Result<Category>.Failure("Categoria nao encontrada", 404);
        if (category.userId != userId) return Result<Category>.Failure("Usuario nao pode acessar categoria de outro usuario", 403);
        return Result<Category>.Success(category, 200);
    }
}