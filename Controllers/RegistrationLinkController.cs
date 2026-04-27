using lievee.Services;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationLinkController : ControllerBase
    {
        private readonly IUniqueCodeService _service;
        public RegistrationLinkController(IUniqueCodeService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateUniqueLink()
        {
            var svc = await _service.GenerateUniqueLinkAsync();
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return Ok(new
            {
                message = "link successfully generated",
                code = svc.Data
            });
        }
    }
}
