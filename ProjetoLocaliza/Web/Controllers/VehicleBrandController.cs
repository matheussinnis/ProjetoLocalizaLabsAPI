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
    public class VehicleBrandController : Controller
    {

        private readonly ILogger<VehicleBrand> _logger;
        private readonly IBaseService<VehicleBrand> _vehicleBrandService;

        public VehicleBrandController(ILogger<VehicleBrand> logger, IBaseService<VehicleBrand> vehicleBrandService)
        {
            _logger = logger;
            _vehicleBrandService = vehicleBrandService;
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] VehicleBrand vehicleBrand)
        {
            try
            {
                await _vehicleBrandService.AddAsync(vehicleBrand);
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
