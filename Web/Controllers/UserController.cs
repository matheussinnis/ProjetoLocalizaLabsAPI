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
    public class UserController : CrudController<User>
    {
        public UserController(ILogger<User> logger, IUserService service)
            : base(logger, service) {}

        [HttpGet]
        [Authorize(Roles = "Operator")]
        public override Task<ObjectResult> GetAll()
        {
            return base.GetAll();
        }

        [HttpPost]
        [AllowAnonymous]
        public override Task<ObjectResult> Create([FromBody] User user)
        {
            return base.Create(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Operator")]
        public override Task<IStatusCodeActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
