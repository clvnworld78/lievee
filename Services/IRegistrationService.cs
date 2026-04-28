using lievee.Models;

namespace lievee.Services
{
    public interface IRegistrationService
    {
        List<RegisteredVisitor> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
        Task<ServiceResultNoData> RegisterVisitorDate(string link, string name, int phoneNumber, DateOnly visitDate);
        Task<ServiceResultNoData> DeleteVisitorData(int visitorId);
    }
}
