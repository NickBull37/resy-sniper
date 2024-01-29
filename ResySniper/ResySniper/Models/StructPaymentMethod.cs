using Newtonsoft.Json;

namespace ResySniper.Models
{
    public struct StructPaymentMethod
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
