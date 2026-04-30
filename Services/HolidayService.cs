using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _repo;
        public HolidayService(IHolidayRepository repo) { _repo = repo; }

        public async Task<ServiceResultNoData> CreateNewHoliday(DateOnly date, Users currentUser)
        {
            var holiday = Holiday.NewHoliday(currentUser.Id, date);

            try
            {
                await _repo.SaveNewHoliday(holiday);
                return ServiceResultNoData.Success();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }

        public async Task<ServiceResult<List<Holiday>>> GetHolidays(DateOnly startDate, DateOnly endDate)
        {
            var data = await _repo.GetHoliday(startDate, endDate);
            if (data.Count == 0)
            {
                return ServiceResult<List<Holiday>>.Failed("No holiday data found", 204);
            }
            
            return ServiceResult<List<Holiday>>.Success(data);
        }

        public async Task<ServiceResultNoData> UpdateHoliday(long holidayId, DateOnly newDate)
        {
            try
            {
                await _repo.UpdateHoliday(holidayId, newDate);
                return ServiceResultNoData.Success();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }

        public async Task<ServiceResultNoData> DeleteHoliday(long holidayId)
        {
            try
            {
                await _repo.DeleteHoliday(holidayId);
                return ServiceResultNoData.Success();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }
    }
}
