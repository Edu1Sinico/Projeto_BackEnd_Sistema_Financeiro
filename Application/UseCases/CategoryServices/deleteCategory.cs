using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryServices;

public class deleteCategory(ICategoryRepository repository)
{
    public async Task<Result<Category>> delete(int id, int userId)
    {
        var category = await repository.GetCategoryAsync(id);
        if (category == null) return Result<Category>.Failure("Categoria nao encontrada", 404);
        if (category.userId != userId) return Result<Category>.Failure("Usuario nao pode excluir categoria de outro usuario", 403);
        if (await repository.IsCategoryInUseAsync(id)) return Result<Category>.Failure("Categoria em uso nao pode ser excluida", 409);
        await repository.DeleteCategoryAsync(category);
        return Result<Category>.NoContent();
    }
}