using lievee.Models;

namespace lievee.Services
{
    public interface IUniqueCodeService
    {
        Task<ServiceResult<string>> GenerateUniqueLinkAsync();
    }
}
