namespace lievee.Models
{
    public class Holiday
    {
        public int? HolidayId { get; set; }
        public int? UserId { get; set; }
        public string? Username { get; set; }
        public required DateOnly Date {  get; set; }


        public static Holiday NewHoliday(int userId, DateOnly date)
        {
            return new Holiday { UserId = userId, Date = date };
        }

        public static Holiday NewRegisteredHoliday(int holidayId, int userId, string username, DateOnly date)
        {
            return new Holiday { HolidayId = holidayId, UserId = userId, Username = username, Date = date };
        }
    }
}
