namespace ResySniper.Configuration
{
    public class ResyConfig
    {
        /// <summary>Found in almost any network call.</summary>
        public string ApiKey { get; } = "VbWk7s3L4KiK5fzlO7JD3Q5EYolJI7n5";


        /// <summary>Found in almost any network call.</summary>
        public string AuthToken { get; } = "eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE3MDg4MDEyMTIsInVpZCI6NDc4NDg1NjYsImd0IjoiY29uc3VtZXIiLCJncyI6W10sImV4dHJhIjp7Imd1ZXN0X2lkIjoxNTI0MTAyMTd9fQ.AEKYQlgwnto5TIeiMmFgpBHPT6H1r-afM0CohWwp9Py_yJFPsZhLzFpTJZQjcsb4Z4EtVKtQQ166ZOKlX4oRV1beAHQ0scHOClZiWQ8TJy2gd8og4MSBCxwo3_6YzVUvcKKRMYEP4t5rowSVNstaoDJh-PipExPLjNoXOOtEbVa2jpsleyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE3MDg4MDEyMTIsInVpZCI6NDc4NDg1NjYsImd0IjoiY29uc3VtZXIiLCJncyI6W10sImV4dHJhIjp7Imd1ZXN0X2lkIjoxNTI0MTAyMTd9fQ.AEKYQlgwnto5TIeiMmFgpBHPT6H1r-afM0CohWwp9Py_yJFPsZhLzFpTJZQjcsb4Z4EtVKtQQ166ZOKlX4oRV1beAHQ0scHOClZiWQ8TJy2gd8og4MSBCxwo3_6YzVUvcKKRMYEP4t5rowSVNstaoDJh-PipExPLjNoXOOtEbVa2jpsl";


        /// <summary>Found in [config?venue_id=*****] network call.</summary>
        /// Don Angie: 1505 [NYC]
        /// Two Fourteen: 59874 [Media]
        /// Stephen's on State: 71149 [Media]
        /// <example>59874</example>
        public int VenueId { get; } = 59874;


        /// <summary>The date you want to make the reservation in YYYY-MM-DD format.</summary>
        /// <example>"2024-02-17"</example>
        public string TargetReservationDate { get; } = "2024-02-17";


        /// <summary>The size of the reservation party.</summary>
        /// <example>2</example>
        public int PartySize { get; } = 2;


        /// <summary>
        ///     Priority list of reservation times and table types. Time must be in military format.
        ///     Key value pairs are in the format <TableType, ReservationTime>.
        ///     Table types are found in the blue rectangles on the venue's booking page. Match the text exactly!
        ///     Table types include..
        ///         "a la carte" --> translates to Window in template slot config
        ///         "Bar Hightop"
        ///         "Booth"
        ///         "Dining Room"
        ///         "Patio"
        /// </summary>
        /// <example>
        ///     new KeyValuePair<string, string>("a la carte", "19:00:00"),
        ///     new KeyValuePair<string, string>("Booth", "19:00:00"),
        ///     new KeyValuePair<string, string>("Dining Room", "19:30:00"),
        /// </example>
        public List<KeyValuePair<string, string>> ReservationSlots { get; } = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("Booth", "19:00:00"),
            new KeyValuePair<string, string>("Dining room", "19:00:00"),
            new KeyValuePair<string, string>("Booth", "19:30:00"),
            new KeyValuePair<string, string>("Dining room", "19:30:00")
        };


        /// <summary>The date & time you want the sniper to wake and snipe reservation in YYYY-MM-DD hh:mm:ss format.</summary>
        /// <example>"2024-01-28 09:00:00"</example>
        public string SnipeTime { get; } = "2024-01-28 20:01:00";
    }
}
