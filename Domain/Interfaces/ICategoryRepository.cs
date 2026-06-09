using Domain.Models;

namespace Domain;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryAsync(int id);
    Task<List<Category>> GetCategoriesAsync(int userId, int page, int quantity);
    Task<bool> ExistsByNameAndTypeAsync(int userId, string name, TransactionType type, int? ignoredId = null);
    Task<bool> IsCategoryInUseAsync(int categoryId);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
}
