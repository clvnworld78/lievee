using lievee.Models;

namespace lievee.Services
{
    public interface IRegistrationService
    {
        Task<ServiceResult<List<RegisteredVisitor>>> GetRegisteredVisitors(DateOnly startDate, DateOnly endDate);
        Task<ServiceResultNoData> RegisterVisitorDate(Guid link, string name, string phoneNumber, DateOnly visitDate);
        Task<ServiceResultNoData> DeleteVisitorData(int visitorId);
    }
}
