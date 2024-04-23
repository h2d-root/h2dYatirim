using Newtonsoft.Json;

namespace h2dYatırım.Entities
{
    public class CoinDataResponse
    {
        [JsonProperty("data")]
        public List<CoinData> Data { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }

}
