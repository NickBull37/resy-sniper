using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationDetails
{
    public class BookToken
    {
        [JsonProperty("book_token")]
        public string Value { get; set; } = string.Empty;
    }
}
