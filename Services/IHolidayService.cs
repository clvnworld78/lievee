using lievee.Models;

namespace lievee.Services
{
    public interface IHolidayService
    {
        Task<ServiceResultNoData> CreateNewHoliday(DateOnly date, Users currentUser);
        Task<ServiceResult<List<Holiday>>> GetHolidays(DateOnly startDate, DateOnly endDate);
        Task<ServiceResultNoData> UpdateHoliday(long holidayId, DateOnly newDate);
        Task<ServiceResultNoData> DeleteHoliday(long holidayId);
    }
}
