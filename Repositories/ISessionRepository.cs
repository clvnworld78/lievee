using lievee.Models;

namespace lievee.Repositories
{
    public interface ISessionRepository
    {
        Task<Users> GetUserAsync(Guid token);
        Task<Users> GetUserCredentialAsync(string username);
        Task SaveToken(Guid token, long userId);
    }
}
