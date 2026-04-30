using lievee.Models;

namespace lievee.Repositories
{
    public interface IRegistrationRepository
    {
        Task<List<RegisteredVisitor>> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
        Task<RegisteredVisitor?> ResolveVisitorId(long visitorId);
        Task SaveNewVisitor(RegisteredVisitor visitor);
        Task DeleteRegisteredVisitor(RegisteredVisitor visitor);
    }
}
