using lievee.Global;
using lievee.Models;
using lievee.Models.Endpoint;
using lievee.Repositories;
using lievee.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        [Authorize(Roles = nameof(UserRole.admin))]
        [HttpGet]
        public async Task<IActionResult> GetRegisteredVisitors([FromQuery] DateOnly? StartDate, [FromQuery] DateOnly? EndDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (StartDate > EndDate)
            {
                return BadRequest(new
                {
                    message = "start date cannot be later than end date"
                });
            }

            var visitors = await _service.GetRegisteredVisitors(StartDate ?? today, EndDate ?? today);
            return Ok(new
            {
                message = "Visitor data is successfully fetched",
                data = visitors.Data
            });
        }

        [HttpPost("{code}")]
        public async Task<IActionResult> RegisterNewVisitor
            (
                [FromRoute] Guid code,
                [FromBody] NewVisitorDataRequest visitorData
            )
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            if (visitorData.VisitDate < today)
            {
                return BadRequest(new
                {
                    message = "visit date already passed"
                });
            }

            var svc = await _service.RegisterVisitorDate(code, visitorData.Name, visitorData.PhoneNumber, visitorData.VisitDate);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return StatusCode(201, new
            {
                message = "successfully registered visit date"
            });
        }

        [Authorize(Roles = nameof(UserRole.admin))]
        [HttpDelete("{visitorId}")]
        public async Task<IActionResult> DeleteRegisteredVisitorData([FromRoute] int visitorId)
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
