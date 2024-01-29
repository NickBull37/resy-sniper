using Newtonsoft.Json;

namespace ResySniper.Models.DeserializationModels.GetReservationTemplates
{
    public class TemplateSlot
    {
        [JsonProperty("config")]
        public TemplateConfig TemplateConfig { get; set; }

        [JsonProperty("date")]
        public TemplateDate TemplateDate { get; set; }

        public bool IsMatching { get; set; }
    }
}
