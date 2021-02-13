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
    public class VehicleCategoryController : Controller
    {

        private readonly ILogger<VehicleCategory> _logger;
        private readonly IBaseService<VehicleCategory> _vehicleCategoryService;

        public VehicleCategoryController(ILogger<VehicleCategory> logger, IBaseService<VehicleCategory> vehicleCategoryService)
        {
            _logger = logger;
            _vehicleCategoryService = vehicleCategoryService;
        }

        [HttpPost]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([FromBody] VehicleCategory vehicleCategory)
        {
            try
            {
                await _vehicleCategoryService.AddAsync(vehicleCategory);
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
