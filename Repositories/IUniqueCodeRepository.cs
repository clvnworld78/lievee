using lievee.Models;

namespace lievee.Repositories
{
    public interface IUniqueCodeRepository
    {
        Task SaveUniqueCode(UniqueCode code);
        Task<long> ResolveLinkIdAsync(Guid uniqueCode);
    }
}
