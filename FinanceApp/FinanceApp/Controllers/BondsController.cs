using System.Security.Claims;
using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BondsController : ControllerBase
    {
        private readonly IBondRepository _bondRepository;

        public BondsController(IBondRepository bondRepository)
        {
            _bondRepository = bondRepository;
        }

        // GET: api/<BondsController>/xyz
        [HttpGet("{userId}")]
        public async Task<IEnumerable<Bond>> GetBondsForUser(string userId)
        {
            return await _bondRepository.GetAllBondsForUser(userId);
        }

        // GET: api/<BondsController>
        [HttpGet]
        public async Task<IEnumerable<Bond>> GetBonds()
        {
            return await _bondRepository.GetAllBonds();
        }

        // POST api/<BondsController>
        [HttpPost]
        public async Task<ActionResult<Bond>> CreateBond([FromBody] Bond bond)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var newBond = await _bondRepository.Create(bond);
            return CreatedAtAction(nameof(GetBonds), new {id = newBond.Id}, newBond);
        }

        // DELETE api/<BondsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var bond = await _bondRepository.GetBondById(id);

            await _bondRepository.Delete(bond.Id, userId);
            return NoContent();
        }

        // GET: api/<BondsController>1//2022-05-01
        [HttpGet("{id:int}/{date}")]
        public async Task<ActionResult<double>> CalculateYield(int id, DateTime date)
        {
            double result = 0.0;
            var bond = await _bondRepository.GetBondById(id);
            
            if (date < bond.PurchaseTime)
            {
                return BadRequest();
            }

            TimeSpan timeSpan = date - bond.PurchaseTime;
            int years = new DateTime(timeSpan.Ticks).Year - 1;

            result = years * ((int)bond.ReturnInterval) * bond.BasePrice * bond.Count * bond.ReturnRate;

            return result;
        }
    }
}