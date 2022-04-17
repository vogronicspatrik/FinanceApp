using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    public class DailyStockSeries
    {
        [JsonProperty("Meta Data")]
        public StockMetaData MetaData { get; set; }
        [JsonProperty("Time Series (Daily)")]
        public Dictionary<DateTime, DailyStockItem> TimeSeries { get; set; }
    }
}