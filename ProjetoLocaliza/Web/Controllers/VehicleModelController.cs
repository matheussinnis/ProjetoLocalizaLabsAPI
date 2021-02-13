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
    public class VehicleModelController : Controller
    {

        private readonly ILogger<VehicleModel> _logger;
        private readonly IBaseService<VehicleModel> _vehicleModelService;

        public VehicleModelController(ILogger<VehicleModel> logger, IBaseService<VehicleModel> vehicleModelService)
        {
            _logger = logger;
            _vehicleModelService = vehicleModelService;
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] VehicleModel vehicleModel)
        {
            try
            {
                await _vehicleModelService.AddAsync(vehicleModel);
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
