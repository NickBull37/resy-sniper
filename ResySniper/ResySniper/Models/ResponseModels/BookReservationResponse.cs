using Newtonsoft.Json;

namespace ResySniper.Models.ResponseModels
{
    public class BookReservationResponse
    {
        [JsonProperty("resy_token")]
        public string ResyToken { get; set; }

        [JsonProperty("reservation_id")]
        public int ReservationId { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
