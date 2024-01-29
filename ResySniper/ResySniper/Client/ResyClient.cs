using Newtonsoft.Json;
using ResySniper.Configuration;
using ResySniper.Models;
using ResySniper.Models.DeserializationModels.GetReservationTemplates;
using ResySniper.Models.RequestModels;
using ResySniper.Models.ResponseModels;
using System.Text;

namespace ResySniper.Client
{
    public class ResyClient : IResyClient
    {
        private readonly ResyConfig _config;

        public ResyClient(ResyConfig config)
        {
            _config = config;
        }

        /// <summary>Makes http request to Resy API and deserializes reservation templates.</summary>
        /// <returns>A GetReservationTemplatesResponse object.</returns>
        public async Task<GetReservationTemplatesResponse> GetReservationTemplatesAsync()
        {
            try
            {
                var requestUrl = "https://api.resy.com/4/find?lat=0&long=0&";
                var queryString = $"day={_config.TargetReservationDate}&party_size={_config.PartySize}&venue_id={_config.VenueId}";
                var resyApiKey = $"ResyAPI api_key=\"{_config.ApiKey}\"";

                using HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
                httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
                httpClient.DefaultRequestHeaders.Add("Accept-Language", "en-US,en;q=0.9");
                httpClient.DefaultRequestHeaders.Add("Authorization", resyApiKey);
                httpClient.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                httpClient.DefaultRequestHeaders.Add("Origin", "https://resy.com");
                httpClient.DefaultRequestHeaders.Add("Referer", "https://resy.com/");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua", "\"Not A(Brand\";v=\"99\", \"Google Chrome\";v=\"121\", \"Chromium\";v=\"121\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Mobile", "?0");
                httpClient.DefaultRequestHeaders.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "same-site");
                httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/121.0.0.0 Safari/537.36");
                httpClient.DefaultRequestHeaders.Add("X-Origin", "https://resy.com");
                httpClient.DefaultRequestHeaders.Add("X-Resy-Auth-Token", _config.AuthToken);
                httpClient.DefaultRequestHeaders.Add("X-Resy-Universal-Auth", _config.AuthToken);

                var completeUrl = requestUrl + queryString;
                // -------------------------------------------------------------------------------------------------------- //
                System.Diagnostics.Debug.WriteLine("[SNIPER LOG] GetReservationTemplatesAsync Logs");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Complete Url: {completeUrl}");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Request Headers: {httpClient.DefaultRequestHeaders}");
                // -------------------------------------------------------------------------------------------------------- //
                var httpResponse = await httpClient.GetAsync(completeUrl);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    GetReservationTemplatesResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "GetReservationTemplatesAsync(): Failure response code from API call."
                            + httpResponse.ReasonPhrase
                            + httpResponse.StatusCode
                    };
                    return response;
                }

                string responseString = await httpResponse.Content.ReadAsStringAsync();

                GetReservationTemplatesResponse? reservationTemplates = JsonConvert.DeserializeObject<GetReservationTemplatesResponse>(responseString);

                if (reservationTemplates is null)
                {
                    GetReservationTemplatesResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ERROR[GetReservationTemplatesAsync]: Failed ReservationTemplates deserialization."
                    };
                    return response;
                }

                reservationTemplates.IsSuccess = true;
                return reservationTemplates;
            }
            catch (Exception ex)
            {
                GetReservationTemplatesResponse response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = "ERROR: Exception occurred during GetReservationTemplates. Message: " + ex.Message
                };
                return response;
            }
        }

        /// <summary>Makes http request to Resy API and deserializes reservation details.</summary>
        /// <param name="template">The reservation template slot for user priority target.</param>
        /// <returns>A GetReservationDetailsResponse object.</returns>
        public async Task<GetReservationDetailsResponse> GetReservationDetailsAsync(TemplateSlot template)
        {
            try
            {
                GetReservationDetailsRequest request = new()
                {
                    Commit = 0,
                    ConfigId = template.TemplateConfig.Token,
                    Day = _config.TargetReservationDate,
                    PartySize = _config.PartySize,
                };

                var requestJson = JsonConvert.SerializeObject(request);

                HttpContent requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                using HttpClient httpClient = new();

                AddHttpClientHeaders(httpClient, "GetReservationDetails");

                var requestUrl = "https://api.resy.com/3/details";

                var httpResponse = await httpClient.PostAsync(requestUrl, requestContent);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    GetReservationDetailsResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ERROR[GetReservationDetails]: Failure response code from API call."
                            + httpResponse.ReasonPhrase
                            + httpResponse.StatusCode
                    };
                    return response;
                }

                string responseString = await httpResponse.Content.ReadAsStringAsync();

                GetReservationDetailsResponse? bookingDetails = JsonConvert.DeserializeObject<GetReservationDetailsResponse>(responseString);

                if (bookingDetails is null)
                {
                    GetReservationDetailsResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ERROR[GetReservationDetails]: Failed BookReservation deserialization."
                    };
                    return response;
                }

                bookingDetails.IsSuccess = true;
                return bookingDetails;
            }
            catch (Exception ex)
            {
                GetReservationDetailsResponse response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = "ERROR: Exception occurred during BookReservation(). Message: " + ex.Message
                };
                return response;
            }
        }

        /// <summary>Makes http request to Resy API and deserializes into booking confirmation response.</summary>
        /// <param name="bookToken">The token needed to book the reservation.</param>
        /// <param name="paymentId">The payment ID of the user (sometimes required for cancelation fees).</param>
        /// <returns>A BookReservationResponse object.</returns>
        public async Task<BookReservationResponse> BookReservationAsync(string bookToken, int paymentId)
        {
            try
            {
                BookReservationRequest request = new()
                {
                    BookToken = bookToken,
                    StructPaymentMethod = new StructPaymentMethod()
                    {
                        Id = paymentId
                    },
                    SourceId = "resy.com-venue-details"
                };

                var requestJson = JsonConvert.SerializeObject(request);

                HttpContent requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");

                using HttpClient httpClient = new();

                AddHttpClientHeaders(httpClient, "BookReservation");

                var requestUrl = "https://api.resy.com/3/book";

                var httpResponse = await httpClient.PostAsync(requestUrl, requestContent);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    BookReservationResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ERROR[BookReservation]: Failure response code from API call."
                            + httpResponse.ReasonPhrase
                            + httpResponse.StatusCode
                    };
                    return response;
                }

                string responseString = await httpResponse.Content.ReadAsStringAsync();

                BookReservationResponse? bookingConfirmation = JsonConvert.DeserializeObject<BookReservationResponse>(responseString);

                if (bookingConfirmation is null)
                {
                    BookReservationResponse response = new()
                    {
                        IsSuccess = false,
                        ErrorMessage = "ERROR: Failed BookReservation deserialization."
                    };
                    return response;
                }

                bookingConfirmation.IsSuccess = true;
                return bookingConfirmation;
            }
            catch (Exception ex)
            {
                BookReservationResponse response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = "ERROR: Exception occurred during BookReservation(). Message: " + ex.Message
                };
                return response;
            }
        }

        /// <summary>Adds request headers to the Http Client.</summary>
        /// <param name="client">The http client.</param>
        /// <param name="methodName">Name of the method using the httpClient determines which headers get added.</param>
        /// <returns>An HttpClient.</returns>
        private HttpClient AddHttpClientHeaders(HttpClient client, string methodName)
        {
            var resyApiKey = $"ResyAPI api_key=\"{_config.ApiKey}\"";

            client.DefaultRequestHeaders.Add("Authorization", resyApiKey);
            client.DefaultRequestHeaders.Add("Origin", "https://widgets.resy.com");
            client.DefaultRequestHeaders.Add("Referer", "https://widgets.resy.com/");
            client.DefaultRequestHeaders.Add("X-Origin", "https://resy.com");
            client.DefaultRequestHeaders.Add("X-Resy-Auth-Token", _config.AuthToken);
            client.DefaultRequestHeaders.Add("X-Resy-Universal-Auth", _config.AuthToken);

            if (methodName == "GetReservationDetails")
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            }
            else if (methodName == "BookReservation")
            {
                client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            }

            return client;
        }
    }
}
