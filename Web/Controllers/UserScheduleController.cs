using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/schedule")]
    public class UserScheduleController : BaseController
    {
        private readonly ILogger<Schedule> _logger;
        private readonly IScheduleService _service;

        public UserScheduleController(ILogger<Schedule> logger, IScheduleService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<ObjectResult> GetUserSchedules(string userId)
        {
            try
            {
                var strValueStartDate = HttpContext.Request.Query["startDate"];
                var startDate = strValueStartDate == StringValues.Empty ?
                    null : strValueStartDate.ToString();

                var strValueEndDate = HttpContext.Request.Query["endDate"];
                var endDate = strValueEndDate == StringValues.Empty ?
                    null : strValueEndDate.ToString();

                return StatusCode(200, await _service.GetUserSchedulesAsync(
                    userId, GetCurrentUserId(), startDate, endDate
                ));
            }

            catch (UnauthorizedException exception)
            {
                return StatusCode(401, new{Message = exception.Message});
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
