using ResySniper.Models.DeserializationModels.GetReservationTemplates;
using ResySniper.Models.ResponseModels;

namespace ResySniper.Client
{
    public interface IResyClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<GetReservationTemplatesResponse> GetReservationTemplatesAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        Task<GetReservationDetailsResponse> GetReservationDetailsAsync(TemplateSlot template);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bookToken"></param>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<BookReservationResponse> BookReservationAsync(string bookToken, int paymentId);
    }
}
