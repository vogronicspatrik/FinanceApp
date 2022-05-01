using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    public class FxMetaData
    {
        [JsonProperty("1. Information")]
        public string Information { get; set; }

        [JsonProperty("2. From Symbol")]
        public string FromSymbol { get; set; }

        [JsonProperty("3. To Symbol")]
        public string ToSymbol { get; set; }

        [JsonProperty("4. Output Size")]
        public string OutputSize { get; set; }

        [JsonProperty("5. Last Refreshed")]
        public string LastRefreshed { get; set; }
        
        [JsonProperty("6. Time Zone")]
        public string TimeZone { get; set; }
    }
}