using System.Security.Claims;
using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _categoryRepository.GetAllCategoriesForUser(userId);
        }

        // POST: api/<CategoriesController>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory([FromBody] Category category)
        {
            category.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newCategory = await _categoryRepository.Create(category);
            return CreatedAtAction(nameof(GetCategories), new {id = newCategory.Id}, newCategory);
        }

        // DELETE api/<CategoriesController>
        [HttpDelete]
        public async Task<ActionResult<int[]>> Delete(int[] ids)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var deletedCategories = new List<int>();
            foreach (var id in ids)
            {
                var categoryToDelete = await _categoryRepository.GetCategoryById(id);

                var success = await _categoryRepository.Delete(categoryToDelete.Id, userId);

                if (success) deletedCategories.Add(id);
            }

            return Ok(deletedCategories);
        }
    }
}