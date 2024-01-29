using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationTemplates
{
    public class TemplateConfig
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
