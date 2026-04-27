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
    }
}
