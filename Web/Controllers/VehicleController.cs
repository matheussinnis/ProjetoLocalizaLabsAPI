using System;
using System.Linq.Expressions;
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
    public class VehicleController : CrudController<Vehicle>
    {
        public new IVehicleService _service { get; set; }

        public VehicleController(
            ILogger<Vehicle> logger, IVehicleService service
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
        public override Task<ObjectResult> Create([FromBody] Vehicle vehicle)
        {
            return base.Create(vehicle);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Update(string id, [FromBody] Vehicle vehicle)
        {
            return base.Update(id, vehicle);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IStatusCodeActionResult> Delete(string id)
        {
            return base.Delete(id);
        }

        [HttpGet("available")]
        [Authorize]
        public async Task<IActionResult> GetAvailableVehicles(string agencyId)
        {
            try
            {
                return StatusCode(200, await _service.GetAvailableVehicles(agencyId));
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
