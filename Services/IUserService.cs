using lievee.Models;

namespace lievee.Services
{
    public interface IUserService
    {
        Task<ServiceResult<long>> CreateNewAdminUserAsync(string username, string password);
        Task<ServiceResult<long>> CreateNewUserUserAsync(string username, string password);
    }
}
