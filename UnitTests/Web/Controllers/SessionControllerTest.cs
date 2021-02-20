using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Requests;
using Infrastructure.Database.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class SessionControllerTest
    {
        private SessionController _controller { get; set; }
        private Mock<ISessionService> _service { get; set; }

        public SessionControllerTest()
        {
            var logger = new Mock<ILogger<SessionController>>();
            _service = new Mock<ISessionService>();
            _controller = new SessionController(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestCreate()
        {
            var document = "x";
            var password = "y";
            var serviceResponse = new object();
            _service
                .Setup(s => s.Create(document, password))
                .Returns(Task.FromResult(serviceResponse));

            var response = await _controller.Create(
                new LoginRequest() { document = document, password = password }
            );

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(serviceResponse, response.Value);
        }

        [Fact]
        public async void TestCreate400()
        {
            var document = "x";
            var password = "y";
            _service
                .Setup(s => s.Create(document, password))
                .Throws(new PasswordMismatchException());

            var response = await _controller.Create(
                new LoginRequest() { document = document, password = password }
            );

            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public async void TestCreate404()
        {
            var document = "x";
            var password = "y";
            _service
                .Setup(s => s.Create(document, password))
                .Throws(new NotFoundException());

            var response = await _controller.Create(
                new LoginRequest() { document = document, password = password }
            );

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async void TestCreate500()
        {
            var document = "x";
            var password = "y";
            _service
                .Setup(s => s.Create(document, password))
                .Throws(new Exception());

            var response = await _controller.Create(
                new LoginRequest() { document = document, password = password }
            );

            Assert.Equal(500, response.StatusCode);
        }
    }
}
