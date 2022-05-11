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
    public class CashFlowItemsController : ControllerBase
    {
        private readonly ICashFlowItemRepository _cashFlowItemRepository;

        public CashFlowItemsController(ICashFlowItemRepository cashFlowItemRepository)
        {
            _cashFlowItemRepository = cashFlowItemRepository;
        }

        // GET: api/<CashFlowItemsController>
        [HttpGet]
        public async Task<IEnumerable<CashFlowItem>> GetCashFlowItems()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _cashFlowItemRepository.GetAllCashFlowItemsForUser(userId);
        }

        // POST: api/<CashFlowItemsController>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateCashFlowItem([FromBody] CashFlowItem item)
        {
            item.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newItem = await _cashFlowItemRepository.Create(item);
            return CreatedAtAction(nameof(GetCashFlowItems), new {id = newItem.Id}, newItem);
        }

        // DELETE api/<CashFlowItemsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _cashFlowItemRepository.Delete(id, userId))
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // GET: api/<CashFlowItemsController>/GetSummary
        [HttpGet("GetSummary")]
        public async Task<IEnumerable<object>> GetBondSum(DateTime from, DateTime to)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _cashFlowItemRepository.GetSummary(userId);
        }
    }
}