using lievee.Models;

namespace lievee.Repositories
{
    public interface ISessionRepository
    {
        Task<Users> GetUserAsync(string token);
        Task<Users> GetUserCredentialAsync(string username);
        Task SaveToken(Guid token, long userId);
    }
}
