using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    public class RealTimeFxItem
    {
        [JsonProperty("1. From_Currency Code")]
        public string FromCurencyCode { get; set; }

        [JsonProperty("2. From_Currency Name")]
        public string FromCurencyName { get; set; }

        [JsonProperty("3. To_Currency Code")]
        public string ToCurencyCode { get; set; }

        [JsonProperty("4. To_Currency Name")]
        public string ToCurencyName { get; set; }

        [JsonProperty("5. Exchange Rate")]
        public double ExchangeRate { get; set; }

        [JsonProperty("6. Last Refreshed")]
        public DateTime LastRefreshed { get; set; }

        [JsonProperty("7. Time Zone")]
        public string TimeZone { get; set; }

        [JsonProperty("8. Bid Price")]
        public double BidPrice { get; set; }

        [JsonProperty("9. Ask Price")]
        public double AskPrice { get; set; }
    }
}