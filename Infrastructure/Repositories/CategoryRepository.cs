using Domain;
using Domain.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CategoryRepository(Context context) : ICategoryRepository
{
    public Task<Category?> GetCategoryAsync(int id) => context.Categories.FindAsync(id).AsTask();
    public Task<List<Category>> GetCategoriesAsync(int userId) => context.Categories.Where(c => c.userId == userId).ToListAsync();
    public Task<bool> ExistsByNameAndTypeAsync(int userId, string name, TransactionType type, int? ignoredId = null) =>
        context.Categories.AnyAsync(c => c.userId == userId && c.name.ToLower() == name.ToLower() && c.type == type && (!ignoredId.HasValue || c.id != ignoredId.Value));
    public Task<bool> IsCategoryInUseAsync(int categoryId) => context.Transactions.AnyAsync(t => t.categoryId == categoryId);

    public async Task CreateCategoryAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }
}
