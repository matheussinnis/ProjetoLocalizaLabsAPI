using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
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

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Create([FromBody] VehicleBrand vehicleBrand)
        {
            return base.Create(vehicleBrand);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Update(string id, [FromBody] VehicleBrand vehicleBrand)
        {
            return base.Update(id, vehicleBrand);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
