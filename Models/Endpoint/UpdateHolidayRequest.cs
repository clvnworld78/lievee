namespace lievee.Models.Endpoint
{
    public class UpdateHolidayRequest
    {
        public long HolidayId { get; set; }
        public DateOnly NewDate { get; set; }
    }
}
