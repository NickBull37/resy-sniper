using Newtonsoft.Json;

namespace ResySniper.Models.RequestModels
{
    public class GetReservationDetailsRequest
    {
        [JsonProperty("commit")]
        public int Commit { get; set; }

        [JsonProperty("config_id")]
        public string ConfigId { get; set; } = string.Empty;

        [JsonProperty("day")]
        public string Day { get; set; } = string.Empty;

        [JsonProperty("party_size")]
        public int PartySize { get; set; }
    }
}
