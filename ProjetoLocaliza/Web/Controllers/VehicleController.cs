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
    public class VehicleController : Controller
    {

        private readonly ILogger<Vehicle> _logger;
        private readonly IBaseService<Vehicle> _vehicleService;

        public VehicleController(ILogger<Vehicle> logger, IBaseService<Vehicle> vehicleService)
        {
            _logger = logger;
            _vehicleService = vehicleService;
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            try
            {
                await _vehicleService.AddAsync(vehicle);
                return StatusCode(201);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new { Message = exception.Message });
            }
        }
    }
}
