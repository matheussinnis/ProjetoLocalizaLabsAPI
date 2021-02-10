using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using api.Domain.ViewModel;
using api.Domain.Entities;
using api.Infra.Database;
using api.Domain.UseCase.UserServices;
using api.Domain.Infra.Database;
using api.Infra.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ILogger<HomeController> _logger;

        public UsersController(EntityContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _userService = new UserService(new UserRepository(context));
        }

        [HttpPost]
        [Route("/users/login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UserLogin userLogin)
        {  
            try
            {
                return StatusCode(200, await _userService.Login(new User(){
                    Email = userLogin.Email,
                    Password = userLogin.Password,
                }, new Token()));
            }
            catch(UserNotFound err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpGet]
        [Route("/users")]
        [Authorize(Roles = "Editor, Administrador")]
        public async Task<ICollection<UserView>> Index()
        {
            return await _userService.All();
        }

        [HttpPost]
        [Route("/users")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                await _userService.Save(user);
                return StatusCode(201);
            }
            catch(UserUniqMail err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpPut]
        [Route("/users/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Update(int id, [FromBody] User user)
        {
            user.Id = id;
            try
            {
                await _userService.Save(user);
                return StatusCode(204);
            }
            catch(UserUniqMail err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
        }

        [HttpDelete]
        [Route("/users/{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.Delete(id);
                return StatusCode(204);
            }
            catch(UserEmptyId err)
            {
                return StatusCode(401, new {
                    Message = err.Message
                });
            }
            catch(UserNotFound err)
            {
                return StatusCode(404, new {
                    Message = err.Message
                });
            }
        }
    }
}
