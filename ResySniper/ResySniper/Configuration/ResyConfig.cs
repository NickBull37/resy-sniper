namespace ResySniper.Configuration
{
    public class ResyConfig
    {
        //  rgs://resy/{venue id}/?/{party size}/{reservation date}/{reservation date}/{reservation time}/{party size}/{dining area}
        public string ResyConfigId { get; } = "rgs://resy/59874/1640581/2/2024-01-18/2024-01-18/21:00:00/2/Dining room";

        public string ApiKey { get; } = "VbWk7s3L4KiK5fzlO7JD3Q5EYolJI7n5";

        public string AuthToken { get; } = "eyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE3MDg4MDEyMTIsInVpZCI6NDc4NDg1NjYsImd0IjoiY29uc3VtZXIiLCJncyI6W10sImV4dHJhIjp7Imd1ZXN0X2lkIjoxNTI0MTAyMTd9fQ.AEKYQlgwnto5TIeiMmFgpBHPT6H1r-afM0CohWwp9Py_yJFPsZhLzFpTJZQjcsb4Z4EtVKtQQ166ZOKlX4oRV1beAHQ0scHOClZiWQ8TJy2gd8og4MSBCxwo3_6YzVUvcKKRMYEP4t5rowSVNstaoDJh-PipExPLjNoXOOtEbVa2jpsleyJ0eXAiOiJKV1QiLCJhbGciOiJFUzI1NiJ9.eyJleHAiOjE3MDg4MDEyMTIsInVpZCI6NDc4NDg1NjYsImd0IjoiY29uc3VtZXIiLCJncyI6W10sImV4dHJhIjp7Imd1ZXN0X2lkIjoxNTI0MTAyMTd9fQ.AEKYQlgwnto5TIeiMmFgpBHPT6H1r-afM0CohWwp9Py_yJFPsZhLzFpTJZQjcsb4Z4EtVKtQQ166ZOKlX4oRV1beAHQ0scHOClZiWQ8TJy2gd8og4MSBCxwo3_6YzVUvcKKRMYEP4t5rowSVNstaoDJh-PipExPLjNoXOOtEbVa2jpsl";

        public int VenueId { get; } = 71149;

        public string ReservationDate { get; } = "2024-01-17";

        public int PartySize { get; } = 2;

        public Dictionary<string, string> ReservationSlots { get; } = new Dictionary<string, string>
        {
            {"Dining room", "18:00:00"},
            {"Dining room", "19:00:00"},
            {"Patio", "19:30:00"}
        };

        public string SnipeTime { get; } = "2024-02-03 09:00:00";
    }
}
