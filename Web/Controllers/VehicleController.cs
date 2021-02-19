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
        public VehicleController(
            ILogger<Vehicle> logger, IBaseService<Vehicle> service
        ) : base(logger, service) {}

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
    }
}
