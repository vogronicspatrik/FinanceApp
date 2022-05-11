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
    public class StocksController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        private readonly AlphaController _alphaController;

        public StocksController(IStockRepository cardRepository)
        {
            _stockRepository = cardRepository;
            _alphaController = new AlphaController();
        }

        // GET: api/<StocksController>
        [HttpGet]
        public async Task<IEnumerable<Stock>> GetStocks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _stockRepository.GetAllStocksForUser(userId);
        }

        // GET api/<StocksController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stock>> GetStock(int id)
        {
            var result = await _stockRepository.GetStockById(id);
            var stockData = await _alphaController.StockData(result.Symbol);
            if(stockData != null && stockData.TimeSeries != null)
            {
                result.CurrentValue = stockData.TimeSeries.First().Value.Close;
            }
            return result;
        }

        // POST api/<StocksController>
        [HttpPost]
        public async Task<ActionResult<Stock>> CreateStock([FromBody] Stock stock)
        {
            stock.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var stockData = await _alphaController.StockData(stock.Symbol);
            if (stockData != null && stockData.TimeSeries != null)
            {
                stock.CurrentValue = stockData.TimeSeries.First().Value.Close;
            }
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

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _stockRepository.Update(stock, userId);

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await _stockRepository.Delete(stockToDelete.Id, userId);
            return NoContent();
        }

        [HttpGet("currentPrice/{id:int}/{currency?}")]
        public async Task<ActionResult<double>> GetCurrentPrice(int id, Currency? currency)
        {   
            double result = 0.0;
            Stock stock = await _stockRepository.GetStockById(id);

            if(stock != null)
            {
                var stockInfo = await _alphaController.StockData(stock.Symbol);
                if (stockInfo.TimeSeries != null)
                {
                    var item = stockInfo.TimeSeries.First();
                    result = stock.Amount * item.Value.Close;

                    if(currency != null)
                    {
                        string currencyStr = currency == Currency.HUF ? "HUF" : "EUR";
                        var fxData = await _alphaController.FxRealTime("USD", currencyStr);
                        if (fxData != null)
                        {
                            result = result * fxData.ExchangeRate;
                        }
                    }
                }
            }

            return result;
        }

        [HttpGet("history/{id:int}/{currency?}")]
        public async Task<ActionResult<Dictionary<string, double>>> GetStockHistory(int id, Currency? currency)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            
            if(currency != null)
            {
                Stock stock = await _stockRepository.GetStockById(id);

                if (stock != null)
                {
                    var stockHistory = await _alphaController.StockRange(stock.Symbol, stock.PurchaseTime, DateTime.Now, currency);
                    
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