using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext masterDbContext)
        {
            _context = masterDbContext;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesForUser(string userId)
        {
            return await _context.categories.Where(category => category.UserId == userId).ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.categories.FindAsync(id);
        }

        public async Task<Category> Create(Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            var categoryToDelete = await _context.categories.FindAsync(id);

            if (categoryToDelete == null || categoryToDelete.UserId != userId) return false;
            
            var transactions = _context.transactions.Where(t => t.CategoryId == id).ToList();
            
            transactions.ForEach(t => t.CategoryId = null);

            _context.categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}