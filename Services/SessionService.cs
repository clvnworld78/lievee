using lievee.Models;
using lievee.Repositories;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<ServiceResult<Guid>> LoginAsync(string username, string rawPassword)
        {
            try
            {
                var user = await _repo.GetUserCredentialAsync(username);
                if (!user.Valid)
                {
                    return ServiceResult<Guid>.Failed("password and username is not valid");
                }

                if (user.Password != HashPasswordAsync(rawPassword))
                {
                    return ServiceResult<Guid>.Failed("password and username is not valid");
                }

                Guid token = Guid.NewGuid();
                await _repo.SaveToken(token, user.Id);

                return ServiceResult<Guid>.Success(token);
            } catch (Exception ex)
            {
                return ServiceResult<Guid>.Failed(ex.Message);
            }
        }

        private byte[] HashPasswordAsync(string password)
        {
            var inputByte = Encoding.UTF8.GetBytes(password);
            return SHA256.HashData(inputByte);
        }

        private string ConvertHashToString(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
