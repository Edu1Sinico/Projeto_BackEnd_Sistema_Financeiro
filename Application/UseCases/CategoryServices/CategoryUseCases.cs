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

public class getCategories(ICategoryRepository repository)
{
    public async Task<Result<List<Category>>> getMany(int userId, int authenticatedUserId)
    {
        if (userId != authenticatedUserId) return Result<List<Category>>.Failure("Usuario nao pode listar categorias de outro usuario", 403);
        return Result<List<Category>>.Success(await repository.GetCategoriesAsync(userId), 200);
    }
}

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
