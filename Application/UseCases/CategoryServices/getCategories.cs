using Application.DTOs;
using Domain;
using Domain.Models;

namespace Application.UseCases.CategoryServices;

public class getCategories(ICategoryRepository repository)
{
    public async Task<Result<CategoryListDTO>> getMany(int userId, int authenticatedUserId,int page, int quantity)
    {
        if (userId != authenticatedUserId) return Result<CategoryListDTO>.Failure("Usuario nao pode listar categorias de outro usuario", 403);
        return Result<CategoryListDTO>.Success(new CategoryListDTO(await repository.GetCategoriesAsync(userId, page,  quantity),page,quantity), 200);
    }
}