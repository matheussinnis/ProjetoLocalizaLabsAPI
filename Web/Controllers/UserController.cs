using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
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

        [HttpGet("{id}")]
        [Authorize]
        public override async Task<ObjectResult> Find(string id)
        {
            try
            {
                var entity = await _service.FindAsync(id, include => include.Address);
                return StatusCode(200, entity);
            }

            catch (NotFoundException exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(404, new{Message = exception.Message});
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
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
