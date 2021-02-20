using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class BaseControllerTest
    {
        class BaseControllerMock : BaseController
        {
            public string Id => GetCurrentUserId();
        }

        private BaseControllerMock _controller { get; set; }

        public BaseControllerTest()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1"),
            }));
            _controller = new BaseControllerMock();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
        }

        [Fact]
        public void GetCurrentUserId()
        {
            Assert.Equal("1", _controller.Id);
        }
    }
}
