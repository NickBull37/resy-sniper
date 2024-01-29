using ResySniper.Models.DeserializationModels.GetReservationTemplates;

namespace ResySniper.Models.ResponseModels
{
    public class GetReservationTemplatesResponse
    {
        public List<TemplateSlot> Slots { get; set; }

        public bool IsSuccess { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
