using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationDetails
{
    public class User
    {
        [JsonProperty("payment_methods")]
        public List<PaymentMethod> PaymentMethods { get; set; } = new();
    }
}
