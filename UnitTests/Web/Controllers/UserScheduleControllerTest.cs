using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class UserScheduleControllerTest
    {
        private UserScheduleController _controller { get; set; }
        private Mock<IScheduleService> _service { get; set; }

        public UserScheduleControllerTest()
        {
            var logger = new Mock<ILogger<Schedule>>();
            _service = new Mock<IScheduleService>();
            _controller = new UserScheduleController(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetUserSchedules()
        {
            var userId = "1";
            var currentUserId = "1";
            var startDate = DateTime.Now.ToString();
            var endDate = DateTime.Now.AddDays(1).ToString();
            _service.Setup(s => s.GetUserSchedulesAsync(
                userId, currentUserId, startDate, endDate
            )).Returns(Task.FromResult(Enumerable.Empty<Schedule>()));

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Query = new QueryCollection(
                new Dictionary<string, StringValues>()
                {
                    { "startDate", new StringValues(startDate) },
                    { "endDate", new StringValues(endDate) },
                }
            );

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var response = await _controller.GetUserSchedules(userId);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<Schedule>(), response.Value);
        }

        [Fact]
        public async void TestGetUserSchedules401()
        {
            var userId = "1";
            var currentUserId = "2";
            var startDate = DateTime.Now.ToString();
            var endDate = DateTime.Now.AddDays(1).ToString();
            _service.Setup(s => s.GetUserSchedulesAsync(
                userId, currentUserId, startDate, endDate
            )).Throws(new UnauthorizedException());

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentUserId),
            }));
            httpContext.Request.Query = new QueryCollection(
                new Dictionary<string, StringValues>()
                {
                    { "startDate", new StringValues(startDate) },
                    { "endDate", new StringValues(endDate) },
                }
            );

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var response = await _controller.GetUserSchedules(userId);

            Assert.Equal(401, response.StatusCode);
        }

        [Fact]
        public async void TestGetUserSchedules500()
        {
            var userId = "1";
            var currentUserId = "2";
            var startDate = DateTime.Now.ToString();
            var endDate = DateTime.Now.AddDays(1).ToString();
            _service.Setup(s => s.GetUserSchedulesAsync(
                userId, currentUserId, startDate, endDate
            )).Throws(new Exception());

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, currentUserId),
            }));
            httpContext.Request.Query = new QueryCollection(
                new Dictionary<string, StringValues>()
                {
                    { "startDate", new StringValues(startDate) },
                    { "endDate", new StringValues(endDate) },
                }
            );

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            var response = await _controller.GetUserSchedules(userId);

            Assert.Equal(500, response.StatusCode);
        }
    }
}
