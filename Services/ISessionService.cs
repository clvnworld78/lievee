using lievee.Models;

namespace lievee.Services
{
    public interface ISessionService
    {
        Task<ServiceResult<Users>> AuthenticateUserAsync(Guid token);
        Task<ServiceResult<Guid>> LoginAsync(string username, string password);
    }
}
