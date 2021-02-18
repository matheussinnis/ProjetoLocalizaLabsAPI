using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetCurrentUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
