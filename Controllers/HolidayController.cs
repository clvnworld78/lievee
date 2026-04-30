using lievee.Global;
using lievee.Models;
using lievee.Models.Endpoint;
using lievee.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [Authorize(Roles = nameof(UserRole.admin))]
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;
        public HolidayController(IHolidayService service) { _service = service; }
        private Users CurrentUser => HttpContext.Items[GlobalConstants.AuthUserCtx] as Users ?? throw new UnauthorizedAccessException();


        [HttpPost]
        public async Task<IActionResult> CreateNewHoliday([FromQuery] DateOnly date)
        {
            var svc = await _service.CreateNewHoliday(date, CurrentUser);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return StatusCode(201, new
            {
                message = "successfully created holiday"
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidays(DateOnly? StartDate, DateOnly? EndDate)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);

            var svc = await _service.GetHolidays(StartDate ?? today, EndDate ?? today);
            if (!svc.IsSuccess)
            {
                return StatusCode(svc.StatusCode, new
                {
                    message = svc.Err
                });
            }

            return Ok(new
            {
                message = "fetching holiday data is successful",
                data = svc.Data
            });
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateHoliday([FromBody] UpdateHolidayRequest newHoliday)
        {
            var svc = await _service.UpdateHoliday(newHoliday.HolidayId, newHoliday.NewDate);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteHoliday([FromQuery] int holidayId)
        {
            var svc = await _service.DeleteHoliday(holidayId);
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
