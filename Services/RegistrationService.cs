using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registRepo;
        private readonly IUniqueCodeRepository _linkRepo;
        private readonly IHolidayRepository _holidayRepo;
        public RegistrationService(IRegistrationRepository repo, IUniqueCodeRepository repo2, IHolidayRepository repo3)
        {
            _registRepo = repo;
            _linkRepo = repo2;
            _holidayRepo = repo3;
        }

        public async Task<ServiceResult<List<RegisteredVisitor>>> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var visitors = await _registRepo.GetRegisteredVisitors(startDate, endDate);
                if (visitors.Count == 0)
                {
                    return ServiceResult<List<RegisteredVisitor>>.Failed("no visitor data found", 204);
                }

                return ServiceResult<List<RegisteredVisitor>>.Success(visitors);
            } catch (Exception ex)
            {
                return ServiceResult<List<RegisteredVisitor>>.Failed(ex.Message, 500);
            }
        }

        public async Task<ServiceResultNoData> RegisterVisitorDate(Guid link, string name, string phoneNumber, DateOnly visitDate)
        {
            try
            {
                var holiday = await _holidayRepo.GetHoliday(visitDate, visitDate);
                if (holiday.Count > 0)
                {
                    return ServiceResultNoData.Failed($"Cannot register a visit during a holiday on {visitDate}");
                }

                var linkId = await _linkRepo.ResolveLinkIdAsync(link);
                var visitor = RegisteredVisitor.NewVisitor(linkId, name, phoneNumber, visitDate);

                await _registRepo.SaveNewVisitor(visitor);
                return ServiceResultNoData.Success();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }

        public async Task<ServiceResultNoData> DeleteVisitorData(int visitorId)
        {
            try
            {
                var registeredVisitor = await _registRepo.ResolveVisitorId(visitorId);
                if (registeredVisitor == null)
                {
                    return ServiceResultNoData.Failed("data not found");
                }

                await _registRepo.DeleteRegisteredVisitor(registeredVisitor);
                return ServiceResultNoData.Success();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }
    }
}
