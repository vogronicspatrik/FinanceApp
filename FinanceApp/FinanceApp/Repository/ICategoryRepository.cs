using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesForUser(string userId);
        Task<Category> GetCategoryById(int id);
        Task<Category> Create(Category category);
        Task<bool> Delete(int id, string userId);
    }
}