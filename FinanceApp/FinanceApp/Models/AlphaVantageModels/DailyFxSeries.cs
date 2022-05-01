using Newtonsoft.Json;

namespace FinanceApp.AlphaVantageModels
{
    public class DailyFxSeries
    {
        [JsonProperty("Meta Data")]
        public FxMetaData MetaData { get; set; }
        
        [JsonProperty("Time Series FX (Daily)")]
        public Dictionary<DateTime, DailyFxItem> TimeSeries { get; set; }
    }
}