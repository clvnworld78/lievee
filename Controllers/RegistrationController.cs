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

        [HttpPost("{code}")]
        public async Task<IActionResult> RegisterNewVisitor(string code, string name, int phoneNumber, DateOnly visitDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (visitDate > today)
            {
                return BadRequest(new
                {
                    message = "visit date cannot be later than today"
                });
            }

            var svc = await _service.RegisterVisitorDate(code, name, phoneNumber, visitDate);

            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return Created();
        }

        [HttpDelete("{visitorId}")]
        public async Task<IActionResult> DeleteRegisteredVisitorData(int visitorId)
        {
            if (visitorId <= 0)
            {
                return BadRequest(new
                {
                    message = "invalid id"
                });
            }

            var svc = await _service.DeleteVisitorData(visitorId);

            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return NoContent();
        }
    }
}
