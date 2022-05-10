using FinanceApp.Enums;
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

        [HttpGet("currentPrice/{id:int}/{currency?}")]
        public async Task<ActionResult<double>> GetCurrentPrice(int id, Currency currency)
        {   
            double result = 0.0;
            Stock stock = await _stockRepository.GetStockById(id);

            if(stock != null)
            {
                AlphaController alphaController = new AlphaController();
                var stockInfo = await alphaController.StockData(stock.Symbol);
                if (stockInfo.TimeSeries != null)
                {
                    var item = stockInfo.TimeSeries.First();
                    result = stock.Amount * item.Value.Close;

                    if(currency != null)
                    {
                        string currencyStr = currency == Currency.HUF ? "HUF" : "EUR";
                        var fxData = await alphaController.FxRealTime("USD", currencyStr);
                        if (fxData == null)
                        {
                            result = result * fxData.ExchangeRate;
                        }
                    }
                }
            }

            return result;
        }

        [HttpGet("history/{id:int}/{currency?}")]
        public async Task<ActionResult<Dictionary<string, double>>> GetStockHistory(int id, Currency currency)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            
            if(currency != null)
            {
                Stock stock = await _stockRepository.GetStockById(id);

                if (stock != null)
                {
                    AlphaController alphaController = new AlphaController();
                    var stockHistory = await alphaController.StockRange(stock.Symbol, stock.PurchaseTime, DateTime.Now, currency);
                    
                    if(stockHistory != null)
                    {
                        foreach (var stockItem in stockHistory.TimeSeries)
                        {
                            result.Add(stockItem.Key.ToString(), stockItem.Value.Close);
                        }
                    }
                }
            }

            return result;
        }
    }
}