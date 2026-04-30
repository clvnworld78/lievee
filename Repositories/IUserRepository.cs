using lievee.Models;

namespace lievee.Repositories
{
    public interface IUserRepository
    {
        Task<long> SaveNewUserAsync(string username, byte[] password, UserRole role);
    }
}
