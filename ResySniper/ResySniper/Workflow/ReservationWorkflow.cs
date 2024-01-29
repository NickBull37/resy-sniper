using ResySniper.Client;
using ResySniper.Configuration;
using ResySniper.Models.DeserializationModels.GetReservationTemplates;
using ResySniper.Models.ResponseModels;
using System.Globalization;

namespace ResySniper.Workflow
{
    public class ReservationWorkflow
    {
        private readonly IResyClient _client;
        private readonly ResyConfig _config;

        public ReservationWorkflow(IResyClient client, ResyConfig config)
        {
            _client = client;
            _config = config;
        }

        /// <summary>Executes the sniper reservation workflow.</summary>
        /// <returns>A Task.</returns>
        public async Task ExecuteSniperWorkflow()
        {
            await SetupReservationSniper();

            await AttemptReservationSnipe();

        }

        /// <summary>Sets up the reservation sniper.</summary>
        /// <returns>A Task.</returns>
        public async Task SetupReservationSniper()
        {
            var currentDate = DateTime.Now;
            var snipeTime = DateTime.ParseExact(_config.SnipeTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            if (snipeTime <= currentDate)
            {
                System.Diagnostics.Debug.WriteLine("[SNIPER LOG] The snipe time has already passed. Skipping ResySniper setup.");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] CurrentDate: {currentDate}");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] SnipeTime: {snipeTime}");
                return;
            }

            TimeSpan waitTime = snipeTime - currentDate;

            System.Diagnostics.Debug.WriteLine("[SNIPER LOG] Setting up ResySniper...");
            System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Target: Venue = {_config.VenueId}, Party Size = {_config.PartySize}, Date = {_config.TargetReservationDate}");
            System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Sleeping for: {waitTime}");

            var milliseconds = (int)waitTime.TotalMilliseconds;
            await Task.Delay(milliseconds);

            var executionTime = DateTime.Now;
            System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Executing ResySniper at: {executionTime}");
        }

        /// <summary>Attempts to snipe a priority reservation target.</summary>
        /// <returns>A Boolean.</returns>
        public async Task<bool> AttemptReservationSnipe()
        {
            System.Diagnostics.Debug.WriteLine("[SNIPER LOG] Attempting to snipe reservation...");

            // STEP 1: Get Reservation Templates
            GetReservationTemplatesResponse reservationTemplates = await _client.GetReservationTemplatesAsync();
            if (!reservationTemplates.IsSuccess)
            {
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG][ERROR] {reservationTemplates.ErrorMessage}");
                return false;
            }

            // STEP 1: Get Reservation Templates
            var matchingSlots = FilterReservationTemplates(reservationTemplates.Slots);
            if (!matchingSlots.Any())
            {
                System.Diagnostics.Debug.WriteLine("[SNIPER LOG][ERROR] No matching reservation templates found.");
                return false;
            }

            // STEP 2: Get Reservation Details
            foreach (var slot in matchingSlots)
            {
                GetReservationDetailsResponse bookingDetails = await _client.GetReservationDetailsAsync(slot);

                if (!bookingDetails.IsSuccess)
                {
                    System.Diagnostics.Debug.WriteLine($"[SNIPER LOG][ERROR]...Failed Step 2: {bookingDetails.ErrorMessage}");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine("[SNIPER LOG]...Reservation details aquired");

                var bookToken = bookingDetails.BookToken.Value;
                var paymentId = bookingDetails.User.PaymentMethods[0].Id;

                // STEP 3: Book Reservation
                BookReservationResponse reservationDetails = await _client.BookReservationAsync(bookToken, paymentId);

                if (!reservationDetails.IsSuccess)
                {
                    System.Diagnostics.Debug.WriteLine($"[SNIPER LOG][ERROR]...Failed Step 3: {reservationDetails.ErrorMessage}");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine("[SNIPER LOG] HEADSHOT! Reservation snipe successfull.");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] ReservationID: {reservationDetails.ReservationId}");
                System.Diagnostics.Debug.WriteLine($"[SNIPER LOG] Resy Token: {reservationDetails.ResyToken}");

                // Reservation booked, skip remaining targets
                return true;
            }

            System.Diagnostics.Debug.WriteLine("[SNIPER LOG] No more reservation targets. Reservation snipe failed.");
            return false;
        }

        /// <summary>Filters the reservation templates based on user priority targets.</summary>
        /// <param name="slots">All reservation slots available for a venue at the date provided.</param>
        /// <returns>A list of TemplateSlots.</returns>
        private List<TemplateSlot> FilterReservationTemplates(List<TemplateSlot> slots)
        {
            List<TemplateSlot> matchingSlots = new();

            foreach (var slot in slots)
            {
                foreach (var priorityTarget in _config.ReservationSlots)
                {
                    if (slot.TemplateConfig.Type == priorityTarget.Key && slot.TemplateDate.DateStart.Contains(priorityTarget.Value))
                    {
                        slot.IsMatching = true;
                        matchingSlots.Add(slot);
                    }
                }
            }

            return matchingSlots;
        }
    }
}
