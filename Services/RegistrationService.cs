using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _registRepo;
        private readonly IUniqueCodeRepository _linkRepo;
        public RegistrationService(IRegistrationRepository repo, IUniqueCodeRepository repo2)
        {
            _registRepo = repo;
            _linkRepo = repo2;
        }

        public List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate)
        {
            return _registRepo.GetRegisteredVisitors(startDate, endDate);
        }

        public async Task<ServiceResultNoData> RegisterVisitorDate(string link, string name, int phoneNumber, DateOnly visitDate)
        {
            try
            {
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
