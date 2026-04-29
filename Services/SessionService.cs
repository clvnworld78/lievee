using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        public SessionService(ISessionRepository repo) { _repo = repo; }

        public async Task<ServiceResult<Users>> AuthenticateUserAsync(string token)
        {
            try
            {
                var user = await _repo.GetUserAsync(token);
                if (!user.Valid)
                {
                    return ServiceResult<Users>.Failed("token is invalid or expired");
                }

                return ServiceResult<Users>.Success(user);
            } catch (Exception ex)
            {
                return ServiceResult<Users>.Failed(ex.Message);
            }
        }
    }
}
