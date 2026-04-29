using lievee.Services;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [ApiController]
    [Route("login")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;
        public SessionController(ISessionService service) { _service = service; }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string username, string password)
        {
            var svc = await _service.LoginAsync(username, password);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return Ok(new
            {
                message = "Login Successful",
                data = svc.Data
            });
        }
    }
}
