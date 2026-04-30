namespace lievee.Models
{
    public class Holiday
    {
        public long? HolidayId { get; set; }
        public long? UserId { get; set; }
        public string? Username { get; set; }
        public required DateOnly Date {  get; set; }


        public static Holiday NewHoliday(long userId, DateOnly date)
        {
            return new Holiday { UserId = userId, Date = date };
        }

        public static Holiday NewRegisteredHoliday(long holidayId, long userId, string username, DateOnly date)
        {
            return new Holiday { HolidayId = holidayId, UserId = userId, Username = username, Date = date };
        }
    }
}
