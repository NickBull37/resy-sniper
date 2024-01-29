using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationTemplates
{
    public class TemplateDate
    {
        [JsonProperty("start")]
        public string DateStart { get; set; }

        [JsonProperty("end")]
        public string DateEnd { get; set; }
    }
}
