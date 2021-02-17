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
    public class VehicleBrandController : CrudController<VehicleBrand>
    {
        public VehicleBrandController(
            ILogger<VehicleBrand> logger, IBaseService<VehicleBrand> service
        ) : base(logger, service) {}

        [HttpGet]
        [Authorize]
        public override async Task<ObjectResult> GetAll()
        {
            try
            {
                return StatusCode(
                    200,
                    HttpContext.Request.Query["name"].Count > 0 ?
                        await _service.FilterAsync(
                            vehicleBrand => vehicleBrand.Name.Contains(
                                HttpContext.Request.Query["name"]
                            )
                        ) : await _service.GetAllAsync()
                );
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Create([FromBody] VehicleBrand vehicleBrand)
        {
            return base.Create(vehicleBrand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> Update(string id, [FromBody] VehicleBrand vehicleBrand)
        {
            return base.Update(id, vehicleBrand);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IStatusCodeActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
