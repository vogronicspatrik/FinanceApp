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

        // GET: api/<BondsController>
        [HttpGet]
        public async Task<IEnumerable<Bond>> GetBonds()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _bondRepository.GetAllBondsForUser(userId);
        }

        // POST: api/<BondsController>
        [HttpPost]
        public async Task<ActionResult<Bond>> CreateBond([FromBody] Bond bond)
        {
            bond.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newBond = await _bondRepository.Create(bond);
            return CreatedAtAction(nameof(GetBonds), new {id = newBond.Id}, newBond);
        }

        // DELETE api/<BondsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _bondRepository.Delete(id, userId))
            {
                return NoContent();
            }

            return BadRequest();
        }
        
        // GET: api/<BondsController>/GetSummary
        [HttpGet("GetSummary")]
        public async Task<IEnumerable<object>> GetBondSum(DateTime from, DateTime to)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _bondRepository.GetSummary(userId);
        }

        // GET: api/<BondsController>1/2022-05-01
        [HttpGet("{id:int}/{date:datetime}")]
        public async Task<ActionResult<double>> CalculateYield(int id, DateTime date)
        {
            double result = 0.0;
            var bond = await _bondRepository.GetBondById(id);

            if (date < bond.MaturityDate)
            {
                return BadRequest();
            }

            TimeSpan timeSpan = date - bond.MaturityDate;
            int years = new DateTime(timeSpan.Ticks).Year - 1;

            result = years * ((int) bond.ReturnInterval + 1) * bond.FaceValue * bond.Count * (bond.ReturnRate / 100);

            return result;
        }
    }
}