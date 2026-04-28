using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository _repo;
        public RegistrationService(IRegistrationRepository repo)
        {
            _repo = repo;
        }

        public List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate)
        {
            return _repo.GetRegisteredVisitors(startDate, endDate);
        }

        public async Task<ServiceResultNoData> RegisterVisitorDate(string link, string name, int phoneNumber, DateOnly visitDate)
        {
            var visitor = new Visitor { LinkCode = link, Name = name, PhoneNumber = phoneNumber, Date = visitDate };

            try
            {
                await _repo.SaveNewVisitor(visitor);
                return ServiceResultNoData.SuccessNoData();
            } catch (Exception ex)
            {
                return ServiceResultNoData.Failed(ex.Message);
            }
        }
    }
}
