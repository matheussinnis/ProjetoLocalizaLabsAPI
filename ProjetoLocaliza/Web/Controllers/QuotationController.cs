using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuotationController : CrudController<Quotation>
    {
        public QuotationController(ILogger<Quotation> logger, IQuotationService service)
            : base(logger, service) {}
    }
}
