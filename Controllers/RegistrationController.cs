using lievee.Models;
using lievee.Repositories;
using lievee.Services;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService _service;
        public RegistrationController(IRegistrationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRegisteredVisitors(DateOnly? startDate, DateOnly? endDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (startDate > endDate)
            {
                return BadRequest(new
                {
                    message = "start date cannot be later than end date"
                });
            }

            var visitors = _service.GetRegisteredVisitors(startDate ?? today, endDate ?? today);
            return Ok(visitors);
        }
    }
}
