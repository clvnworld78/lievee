using lievee.Models;

namespace lievee.Services
{
    public interface ISessionService
    {
        Task<ServiceResult<Users>> AuthenticateUserAsync(string token);
    }
}
