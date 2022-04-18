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
        public async Task<Category> Create(Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task Delete(int Id)
        {
            var categoryToDelete = await _context.categories.FindAsync(Id);
            _context.categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.categories.ToListAsync();
        }

        public async Task<Category> GetCategoryById(int Id)
        {
            return await _context.categories.FindAsync(Id);
        }
    }
}
