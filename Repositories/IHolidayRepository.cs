using lievee.Models;

namespace lievee.Repositories
{
    public interface IHolidayRepository
    {
        Task SaveNewHoliday(Holiday newHoliday);
        Task<List<Holiday>> GetHoliday(DateOnly startDate, DateOnly endDate);
        Task UpdateHoliday(long holidayId, DateOnly newDate);
        Task DeleteHoliday(long holidayId);
    }
}
