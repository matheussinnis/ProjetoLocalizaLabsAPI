using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : CrudController<Schedule>
    {
        public ScheduleController(ILogger<Schedule> logger, IScheduleService service)
            : base(logger, service) {}
    }
}
