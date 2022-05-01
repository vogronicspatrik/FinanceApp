using FinanceApp.Models;
using FinanceApp.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinanceApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StocksController(IStockRepository cardRepository)
        {
            _stockRepository = cardRepository;
        }

        // GET: api/<StocksController>
        [HttpGet]
        public async Task<IEnumerable<Stock>> GetStocks()
        {
            return await _stockRepository.GetAllStocks();
        }

        // GET api/<StocksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            return await _stockRepository.GetStockById(id);
        }

        // POST api/<StocksController>
        [HttpPost]
        public async Task<ActionResult<Stock>> CreateStock([FromBody] Stock stock)
        {
            var newStock = await _stockRepository.Create(stock);
            return CreatedAtAction(nameof(GetStocks), new { id = newStock.Id }, newStock);
        }

        // PUT api/<StocksController>/5
        [HttpPut]
        public async Task<ActionResult> UpdateStock(int id, [FromBody] Stock stock)
        {
            if (id != stock.Id)
            {
                return BadRequest();
            }

            await _stockRepository.Update(stock);

            return NoContent();
        }

        // DELETE api/<StocksController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var stockToDelete = await _stockRepository.GetStockById(id);
            if (stockToDelete == null)
            {
                return NotFound();
            }
            await _stockRepository.Delete(stockToDelete.Id);
            return NoContent();
        }
    }
}