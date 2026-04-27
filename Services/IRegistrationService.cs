using lievee.Models;

namespace lievee.Services
{
    public interface IRegistrationService
    {
        List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
    }
}
