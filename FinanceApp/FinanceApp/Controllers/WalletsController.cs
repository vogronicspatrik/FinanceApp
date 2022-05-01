using System.Security.Claims;
using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;

        public WalletsController(IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }

        // GET: api/<WalletsController>
        [HttpGet]
        public async Task<IEnumerable<Wallet>> GetWallets()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _walletRepository.GetAllWalletsForUser(userId);
        }

        // GET api/<WalletsController>/5/2022-02-16/2022-03-16
        [HttpGet("{id:int}/{from:datetime}/{to:datetime}")]
        public async Task<ActionResult<Wallet>> GetWallet(int id, DateTime from, DateTime to)
        {
            return await _walletRepository.GetWalletById(id, from, to);
        }

        // POST api/<WalletsController>
        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet([FromBody] Wallet wallet)
        {
            wallet.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var newWallet = await _walletRepository.Create(wallet);
            return CreatedAtAction(nameof(GetWallets), new {id = newWallet.Id}, newWallet);
        }

        // PUT api/<WalletsController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateWallet(int id, [FromBody] Wallet wallet)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _walletRepository.Update(id, wallet, userId))
            {
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE api/<WalletsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (await _walletRepository.Delete(id, userId))
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}