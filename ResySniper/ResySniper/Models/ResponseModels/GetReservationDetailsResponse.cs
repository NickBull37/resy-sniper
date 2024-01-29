using Newtonsoft.Json;
using ResySniper.Models.DeserializationModels.GetReservationDetails;

namespace ResySniper.Models.ResponseModels
{
    public class GetReservationDetailsResponse
    {
        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("book_token")]
        public BookToken BookToken { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
