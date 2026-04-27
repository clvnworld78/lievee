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

        public async Task<ServiceResult<string>> GenerateUniqueLink()
        {
            Guid uuidv4 = Guid.NewGuid();
            var uniqueCode = new UniqueCode { Code = uuidv4 };
            try
            {
                await _repo.SaveUniqueCode(uniqueCode);
                return ServiceResult<string>.Success(uuidv4.ToString());
            } catch (Exception ex)
            {
                return ServiceResult<string>.Failed(ex.Message);
            }
        }
    }
}
