using lievee.Models;
using lievee.Repositories;

namespace lievee.Services
{
    public class UniqueCodeService : IUniqueCodeService
    {
        private readonly IUniqueCodeRepository _repo;
        public UniqueCodeService(IUniqueCodeRepository repo)
        {
            _repo = repo;
        }

        public async Task<ServiceResult<string>> GenerateUniqueLinkAsync()
        {
            Guid uuidV4 = Guid.NewGuid();
            var code = new UniqueCode { Code = uuidV4 };

            try
            {
                await _repo.SaveUniqueCode(code);
                return ServiceResult<string>.Success(uuidV4.ToString());
            } catch (Exception ex)
            {
                return ServiceResult<string>.Failed(ex.Message);
            }
        }
    }
}
