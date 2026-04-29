using lievee.Models;

namespace lievee.Repositories
{
    public interface ISessionRepository
    {
        Task<Users> GetUserAsync(string token);
    }
}
