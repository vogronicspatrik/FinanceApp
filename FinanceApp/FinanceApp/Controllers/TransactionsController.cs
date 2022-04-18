using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {

        private readonly ITransactionRepository _transactionRepository;

        public TransactionsController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
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
            var newTransaction = await _transactionRepository.Create(transaction);
            return CreatedAtAction(nameof(GetTransactions), new { id = newTransaction.Id }, newTransaction);
        }

        // DELETE api/<TransactionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var transactionToDelete = await _transactionRepository.GetTransactionById(id);
            if (transactionToDelete == null)
            {
                return NotFound();
            }
            await _transactionRepository.Delete(transactionToDelete.Id);
            return NoContent();
        }

    }
}
