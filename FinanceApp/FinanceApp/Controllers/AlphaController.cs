using Microsoft.AspNetCore.Mvc;
using FinanceApp.AlphaVantageModels;
using Newtonsoft.Json;
using FinanceApp.Enums;

namespace FinanceApp.Controllers
{

    /*
    Functions to support:
    DIGITAL_CURRENCY_DAILY
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

        public async Task<DailyFxSeries> FxDailyData(string fromSymbol, string toSymbol)
        {
            string queryString = $"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol={fromSymbol}&to_symbol={toSymbol}&outputsize=full&apikey={APIKEY}";
            Uri uri = new Uri(queryString);

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                if (jsonStr != null)
                {
                    DailyFxSeries fxItem = JsonConvert.DeserializeObject<DailyFxSeries>(jsonStr);
                    return fxItem;
                }
            }
            return null;
        }

        public async Task<RealTimeFxItem> FxRealTime(string fromSymbol, string toSymbol)
        {
            string queryString = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromSymbol}&to_currency={toSymbol}&apikey={APIKEY}";
            Uri uri = new Uri(queryString);

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var jsonStr = await response.Content.ReadAsStringAsync();
                if (jsonStr != null)
                {
                    RealTimeFxData fxData = JsonConvert.DeserializeObject<RealTimeFxData>(jsonStr);
                    return fxData.FxItem;
                }
            }
            return null;
        }

        // Convert the price of all stock items to the given currency
        public async Task<DailyStockSeries> ConvertStockPrice(DailyStockSeries data, Currency? currency)
        {
            if(currency == null)
            {
                return data;
            }

            string currencyStr = currency == Currency.HUF ? "HUF" : "EUR";
            var fxData = await FxDailyData("USD", currencyStr);

            if (data == null || data.TimeSeries == null || fxData == null || fxData.TimeSeries == null)
            {
                return data;
            }

            foreach(var item in data.TimeSeries)
            {
                DateTime date = item.Key;
                
                if (fxData.TimeSeries.ContainsKey(date))
                {
                    data.TimeSeries[date].Open = item.Value.Open * fxData.TimeSeries[date].Open;
                    data.TimeSeries[date].Low = item.Value.Low * fxData.TimeSeries[date].Low;
                    data.TimeSeries[date].High = item.Value.High * fxData.TimeSeries[date].High;
                    data.TimeSeries[date].Close = item.Value.Close * fxData.TimeSeries[date].Close;
                }
            }

            return data;
        }

        [HttpGet("stock/{symbol}/{currency?}")]
        public async Task<ActionResult<DailyStockSeries>> Stock(string symbol, Currency? currency)
        {
            DailyStockSeries stockData = await StockData(symbol);
            
            if (currency != null && stockData != null)
            {
                stockData = await ConvertStockPrice(stockData, currency);
            }

            if(stockData != null)
            {
                return Ok(stockData);
            }

            return BadRequest();
        }

        [HttpGet("stockRange/{symbol}/{fromDate}/{toDate}/{currency?}")]
        public async Task<ActionResult<DailyStockSeries>> StockRange(string symbol, DateTime fromDate, DateTime toDate, Currency? currency)
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

                if(currency != null)
                {
                    result = await ConvertStockPrice(result, currency);
                }

                return Ok(result);
            }

            return BadRequest();
        }

        // returns historical exchange rate
        [HttpGet("fx/{fromSymbol}/{toSymbol}/{fromDate}/{toDate}")]
        public async Task<ActionResult<DailyFxSeries>> Fx(string fromSymbol, string toSymbol, DateTime fromDate, DateTime toDate)
        {
            DailyFxSeries allItems = await FxDailyData(fromSymbol, toSymbol);

            if(allItems != null && allItems.TimeSeries != null)
            {
                Dictionary<DateTime, DailyFxItem> resultTimeSeries;

                resultTimeSeries = allItems.TimeSeries
                    .Where(i => i.Key >= fromDate && i.Key <= toDate)
                    .OrderBy(i => i.Key)
                    .ToDictionary(i => i.Key, i => i.Value);

                DailyFxSeries result = new DailyFxSeries
                {
                    MetaData = allItems.MetaData,
                    TimeSeries = resultTimeSeries
                };

                return Ok(result);
            }

            return BadRequest();
        }

        // return realtime exchange rate
        [HttpGet("fxreal/{fromSymbol}/{toSymbol}")]
        public async Task<ActionResult<RealTimeFxItem>> RealTimeFx(string fromSymbol, string toSymbol)
        {
            RealTimeFxItem fxItem = await FxRealTime(fromSymbol, toSymbol);
            if (fxItem != null)
            {
                return Ok(fxItem);
            }

            return BadRequest();
        }
    }
}