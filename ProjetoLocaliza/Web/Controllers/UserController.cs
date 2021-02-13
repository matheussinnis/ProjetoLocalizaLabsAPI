using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<User> _logger;
        private readonly IBaseService<User> _userService;

        public UserController(ILogger<User> logger, IBaseService<User> userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> GetAll(int? type)
        {
            try
            {
                IEnumerable<User> users;
                
                if (type == null) users = await _userService.GetAllAsync();
                else users = await _userService.FilterAsync(user => (int) user.Type == type);
                
                return StatusCode(200, users);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] User address)
        {
            try
            {
                await _userService.AddAsync(address);
                return StatusCode(201);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
