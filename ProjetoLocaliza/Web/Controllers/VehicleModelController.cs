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
    public class VehicleModelController : CrudController<VehicleModel>
    {
        public VehicleModelController(
            ILogger<VehicleModel> logger, IBaseService<VehicleModel> service
        ) : base(logger, service) {}

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Create([FromBody] VehicleModel vehicleModel)
        {
            return base.Create(vehicleModel);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Update(string id, [FromBody] VehicleModel vehicleModel)
        {
            return base.Update(id, vehicleModel);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
