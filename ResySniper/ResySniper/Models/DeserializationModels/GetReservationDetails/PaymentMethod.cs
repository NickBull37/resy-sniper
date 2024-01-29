using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationDetails
{
    public class PaymentMethod
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
