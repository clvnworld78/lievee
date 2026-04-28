using lievee.Models;

namespace lievee.Repositories
{
    public interface IRegistrationRepository
    {
        List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
        Task<RegisteredVisitor?> ResolveVisitorId(int visitorId);
        Task SaveNewVisitor(RegisteredVisitor visitor);
        Task DeleteRegisteredVisitor(RegisteredVisitor visitor);
    }
}
