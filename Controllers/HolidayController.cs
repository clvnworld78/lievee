using lievee.Global;
using lievee.Models;
using lievee.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lievee.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    [ApiController]
    [Route("[controller]")]
    public class HolidayController : ControllerBase
    {
        private readonly IHolidayService _service;
        public HolidayController(IHolidayService service) { _service = service; }
        private Users CurrentUser => HttpContext.Items[GlobalConstants.AuthUserCtx] as Users ?? throw new UnauthorizedAccessException();


        [HttpPost]
        public async Task<IActionResult> CreateNewHoliday(DateOnly date)
        {
            var svc = await _service.CreateNewHoliday(date, CurrentUser);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
                {
                    message = svc.Err
                });
            }

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidays(DateOnly startDate, DateOnly endDate)
        {
            var svc = await _service.GetHolidays(startDate, endDate);
            if (!svc.IsSuccess)
            {
                return StatusCode(500, new
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
        public async Task<IActionResult> UpdateHoliday(int holidayId, DateOnly newDate)
        {
            var svc = await _service.UpdateHoliday(holidayId, newDate);
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
        public async Task<IActionResult> DeleteHoliday(int holidayId)
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
