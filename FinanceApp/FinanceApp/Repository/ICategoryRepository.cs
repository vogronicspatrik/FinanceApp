using FinanceApp.Models;

namespace FinanceApp.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<Category> Create(Category category);
        Task Delete(int Id);
        Task<Category> GetCategoryById(int Id);
    }
}
