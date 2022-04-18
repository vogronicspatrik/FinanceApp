using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepositoy)
        {
            _categoryRepository = categoryRepositoy;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            var newCategory = await _categoryRepository.Create(category);
            return CreatedAtAction(nameof(GetCategories), new { id = newCategory.Id }, newCategory);

        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var categoryToDelete = await _categoryRepository.GetCategoryById(id);
            if (categoryToDelete == null)
            {
                return NotFound();
            }
            await _categoryRepository.Delete(categoryToDelete.Id);
            return NoContent();
        }
    }
}
