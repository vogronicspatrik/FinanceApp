using FinanceApp.Models;
using FinanceApp.Repository;
using FinanceApp.Repository.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IWalletRepository _walletRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletsController(IWalletRepository walletRepository, ITransactionRepository transactionRepository)
        {
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
        }

        // GET: api/<CardsController>
        [HttpGet]
        public async Task<IEnumerable<WalletDTO>> GetWallets()
        {
            var wallets = from wallet in await _walletRepository.GetAllWallets()
                          select new WalletDTO()
                          {
                              AccountNumber = wallet.AccountNumber,
                              Owner = wallet.Owner,
                              Type = wallet.Type,
                              Balance = wallet.Balance,
                              Location = wallet.Location,
                              Currency = wallet.Currency,
                              TransactionList =  _transactionRepository.GetLastFiveTransaction(wallet.Id).Result.ToList()
                          };
            return wallets;
        }

        // GET api/<CardsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wallet>> GetWallet(int id)
        {
            return await _walletRepository.GetWalletById(id);
        }

        // POST api/<CardsController>
        [HttpPost]
        public async Task<ActionResult<Wallet>> CreateWallet([FromBody] Wallet wallet)
        {
            var newWallet = await _walletRepository.Create(wallet);
            return CreatedAtAction(nameof(GetWallets), new { id = newWallet.Id }, newWallet);

        }

        // PUT api/<CardsController>/5
        [HttpPut]
        public async Task<ActionResult> UpdateWallet(int id, [FromBody] Wallet wallet)
        {
            if(id != wallet.Id)
            {
                return BadRequest();
            }

            await _walletRepository.Update(wallet);

            return NoContent();
        }

        // DELETE api/<CardsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var walletToDelete = await _walletRepository.GetWalletById(id);
            if(walletToDelete == null)
            {
                return NotFound();
            }
            await _walletRepository.Delete(walletToDelete.Id);
            return NoContent();
        }
    }
}
