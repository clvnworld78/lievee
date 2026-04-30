using lievee.Helper;
using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;
        public UserService(IUserRepository repo) { _repo = repo; }

        public async Task<ServiceResult<long>> CreateNewAdminUserAsync(string username, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            try
            {
                var userId = await _repo.SaveNewUserAsync(username, hashedPassword, UserRole.admin);
                if (userId == 0)
                {
                    return ServiceResult<long>.Failed("repo failed saving a new user", 500);
                }

                return ServiceResult<long>.Success(userId);
            } catch (Exception ex)
            {
                return ServiceResult<long>.Failed(ex.Message, 500);
            }
        }

        public async Task<ServiceResult<long>> CreateNewUserUserAsync(string username, string password)
        {
            var hashedPassword = PasswordHasher.HashPassword(password);
            try
            {
                var userId = await _repo.SaveNewUserAsync(username, hashedPassword, UserRole.user);
                if (userId == 0)
                {
                    return ServiceResult<long>.Failed("repo failed saving a new user", 500);
                }

                return ServiceResult<long>.Success(userId);
            }
            catch (Exception ex)
            {
                return ServiceResult<long>.Failed(ex.Message, 500);
            }
        }
    }
}
