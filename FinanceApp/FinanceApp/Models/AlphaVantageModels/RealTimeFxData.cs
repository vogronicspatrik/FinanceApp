using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    // used only for easier deserialization
    public class RealTimeFxData
    {
        [JsonProperty("Realtime Currency Exchange Rate")]
        public RealTimeFxItem FxItem { get; set; }
    }
}