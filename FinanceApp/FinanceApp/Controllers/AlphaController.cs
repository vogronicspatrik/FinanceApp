using Microsoft.AspNetCore.Mvc;
using System.Text;
using FinanceApp.AlphaVantageModels;
using System.Text.Json;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FinanceApp.Controllers
{

    /*
    Functions to support:
    - TIME_SERIES_DAILY
    SYMBOL_SEARCH
    CURRENCY_EXCHANGE_RATE
    FX_DAILY
    DIGITAL_CURRENCY_DAILY
    INFLATION_EXPECTATION
    */

    [ApiController]
    [Route("api/[controller]")]
    public class AlphaController : ControllerBase
    {
        private const string APIKEY = "BRJ4GQ9MKH1K08JX"; // read from config
        private HttpClient _httpClient = new HttpClient();

        public async Task<DailyStockSeries> StockData(string symbol)
        {
            string queryString = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={symbol}&outputsize=full&apikey={APIKEY}";
            Uri uri = new Uri(queryString);

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                if (jsonStr != null)
                {
                    DailyStockSeries stockItem = JsonConvert.DeserializeObject<DailyStockSeries>(jsonStr);
                    return stockItem;
                }
            }
            return null;
        }

        [HttpGet("stock/{symbol}")]
        public async Task<ActionResult<DailyStockSeries>> Stock(string symbol)
        {
            DailyStockSeries stockData = await StockData(symbol);

            if(stockData != null)
            {
                return Ok(stockData);
            }

            return BadRequest();
        }

        [HttpGet("stockRange/{symbol}/{fromDate}:{toDate}")]
        public async Task<ActionResult<DailyStockSeries>> StockRange(string symbol, DateTime fromDate, DateTime toDate)
        {
            DailyStockSeries allItems = await StockData(symbol);

            if (allItems != null && allItems.TimeSeries != null)
            {
                Dictionary<DateTime, DailyStockItem> resultTimeSeries = allItems.TimeSeries
                    .Where(i => i.Key >= fromDate && i.Key <= toDate)
                    .OrderBy(i => i.Key)
                    .ToDictionary(i => i.Key, i => i.Value);

                DailyStockSeries result = new DailyStockSeries
                {
                    MetaData = allItems.MetaData,
                    TimeSeries = resultTimeSeries
                };

                return Ok(result);
            }

            return BadRequest();
        }
    }
}