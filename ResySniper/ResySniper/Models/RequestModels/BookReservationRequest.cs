using Newtonsoft.Json;

namespace ResySniper.Models.RequestModels
{
    public class BookReservationRequest
    {
        [JsonProperty("book_token")]
        public string BookToken { get; set; }

        [JsonProperty("struct_payment_method")]
        public StructPaymentMethod StructPaymentMethod { get; set; }

        [JsonProperty("source_id")]
        public string SourceId { get; set; }
    }
}
