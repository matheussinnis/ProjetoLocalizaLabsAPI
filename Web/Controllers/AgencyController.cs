using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgencyController : CrudController<Agency>
    {
        private new IAgencyService _service { get; set; }

        public AgencyController(
            ILogger<Agency> logger, IAgencyService service
        ) : base(logger, service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public override Task<ObjectResult> GetAll()
        {
            return base.GetAll();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public override Task<ObjectResult> Find(string id)
        {
            return base.Find(id);
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Create([FromBody] Agency agency)
        {
            return base.Create(agency);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Update(string id, [FromBody] Agency agency)
        {
            return base.Update(id, agency);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IStatusCodeActionResult> Delete(string id)
        {
            return base.Delete(id);
        }

        [HttpGet("{id}/vehicles")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVehicles(
            string id, bool available, DateTime withdrawalDate,
            int page = 1, int perPage = 10
        )
        {
            try
            {
                return StatusCode(200,
                    await _service.GetAvailableVehicles(
                        id, available, withdrawalDate, page, perPage
                    )
                );
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpGet("nearest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNearestAgencies(
            float latitude, float longitude
        )
        {
            try
            {
                return StatusCode(
                    200,
                    await _service.GetNearestAgencies(latitude, longitude)
                );
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
