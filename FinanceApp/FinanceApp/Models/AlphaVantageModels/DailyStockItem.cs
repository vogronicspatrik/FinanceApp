using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    public class DailyStockItem
    {
        [JsonProperty("1. open")]
        public double Open { get; set; }
        [JsonProperty("2. high")]
        public double High { get; set; }
        [JsonProperty("3. low")]
        public double Low { get; set; }
        [JsonProperty("4. close")]
        public double Close { get; set; }
        [JsonProperty("5. volume")]
        public double Volume { get; set; }
    }
}