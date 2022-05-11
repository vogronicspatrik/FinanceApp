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
    public class ValuableAssetsController : ControllerBase
    {
        private readonly IValuableAssetRepository _valuableAssetRepository;

        public ValuableAssetsController(IValuableAssetRepository valuableAssetRepository)
        {
            _valuableAssetRepository = valuableAssetRepository;
        }

        // GET: api/<ValuableAssetsController>
        [HttpGet]
        public async Task<IEnumerable<ValuableAsset>> GetValuableAssets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _valuableAssetRepository.GetAllAssetsForUser(userId);
        }

        // POST: api/<ValuableAssetsController>
        [HttpPost]
        public async Task<ActionResult<Category>> CreateValuableAsset([FromBody] ValuableAsset asset)
        {
            asset.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newAsset = await _valuableAssetRepository.Create(asset);
            return CreatedAtAction(nameof(GetValuableAssets), new {id = newAsset.Id}, newAsset);
        }

        // DELETE api/<ValuableAssetsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _valuableAssetRepository.Delete(id, userId))
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // GET: api/<ValuableAssetsController>/GetSummary
        [HttpGet("GetSummary")]
        public async Task<IEnumerable<object>> GetWalletSum(DateTime from, DateTime to)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _valuableAssetRepository.GetSummary(userId);
        }
    }
}