using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChecklistController : CrudController<Checklist>
    {
        public ChecklistController(ILogger<Checklist> logger, IBaseService<Checklist> service)
            : base(logger, service) {}
    }
}
