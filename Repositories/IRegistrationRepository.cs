using lievee.Models;

namespace lievee.Repositories
{
    public interface IRegistrationRepository
    {
        List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
        Task SaveNewVisitor(Visitor visitor);
    }
}
