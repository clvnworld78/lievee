using lievee.Global;
using lievee.Models.Endpoint;
using lievee.Services;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService service) { _service = service; }

        [HttpPost("admin")]
        public async Task<IActionResult> CreateNewAdminUser([FromBody] UserLoginInfoRequest loginData)
        {
            var svc = await _service.CreateNewAdminUserAsync(loginData.Username, loginData.Password);
            if (!svc.IsSuccess)
            {
                return StatusCode(svc.StatusCode, new
                {
                    message = svc.Err
                });
            }

            return Ok(new
            {
                message = "User is successfully created",
                userId = svc.Data
            });
        }

        [HttpPost("user")]
        public async Task<IActionResult> CreateNewUser([FromBody] UserLoginInfoRequest loginData)
        {
            var svc = await _service.CreateNewUserUserAsync(loginData.Username, loginData.Password);
            if (!svc.IsSuccess)
            {
                return StatusCode(svc.StatusCode, new
                {
                    message = svc.Err
                });
            }

            return Ok(new
            {
                message = "User is successfully created",
                userId = svc.Data
            });
        }
    }
}
