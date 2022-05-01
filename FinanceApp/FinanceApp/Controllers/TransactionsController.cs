using System.Security.Claims;
using FinanceApp.Enums;
using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IWalletRepository _walletRepository;

        public TransactionsController(ITransactionRepository transactionRepository, IWalletRepository walletRepository)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
        }

        // GET: api/<TransactionsController>
        [HttpGet]
        public async Task<IEnumerable<Transaction>> GetTransactions()
        {
            return await _transactionRepository.GetAllTransactions();
        }

        // POST api/<TransactionsController>
        [HttpPost]
        public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] Transaction transaction)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var price = transaction.Type == TransactionType.INCOME ? transaction.Price : -transaction.Price;

            if (!await _walletRepository.UpdateBalance(transaction.WalletId, price, userId)) return BadRequest();

            var newTransaction = await _transactionRepository.Create(transaction);
            return CreatedAtAction(nameof(GetTransactions), new {id = newTransaction.Id}, newTransaction);
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var transaction = await _transactionRepository.GetTransactionById(id);
            var price = transaction.Type == TransactionType.INCOME ? -transaction.Price : transaction.Price;

            if (!await _walletRepository.UpdateBalance(transaction.WalletId, price, userId)) return BadRequest();

            await _transactionRepository.Delete(transaction.Id);
            return NoContent();
        }
    }
}