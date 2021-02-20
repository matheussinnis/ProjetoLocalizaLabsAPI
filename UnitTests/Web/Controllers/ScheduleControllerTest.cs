using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class ScheduleControllerTest
    {
        private Mock<ILogger<Schedule>> _logger { get; set; }
        private ScheduleController _controller { get; set; }
        private Mock<IScheduleService> _service { get; set; }
        private Mock<INodeServices> _nodeService { get; set; }

        public ScheduleControllerTest()
        {
            _logger = new Mock<ILogger<Schedule>>();
            _service = new Mock<IScheduleService>();
            _nodeService = new Mock<INodeServices>();
            _controller = new ScheduleController(_logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetAll()
        {
            _service.Setup(s => s.GetAllAsync()).Returns(
                Task.FromResult(Enumerable.Empty<Schedule>())
            );

            var response = await _controller.GetAll();

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<Schedule>(), response.Value);
        }

        [Fact]
        public async void TestGetAllStatus500()
        {
            _service.Setup(s => s.GetAllAsync()).Throws(new Exception());

            var response = await _controller.GetAll();

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestFind()
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var modelMock = new Schedule() { Id = guid };
            _service.Setup(s => s.FindAsync(id)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Find(id);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestFind404()
        {
            var id = "";
            _service.Setup(s => s.FindAsync(id)).Throws(new NotFoundException());

            var response = await _controller.Find(id);

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async void TestFind500()
        {
            var id = "";
            _service.Setup(s => s.FindAsync(id)).Throws(new Exception());

            var response = await _controller.Find(id);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestCreate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Schedule() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Create(modelMock);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestCreate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Schedule() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Throws(new Exception());

            var response = await _controller.Create(modelMock);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestUpdate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Schedule() { Id = guid };
            _service.Setup(s => s.UpdateAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Update(modelMock.Id.ToString(), modelMock);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestUpdate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Schedule() { Id = guid };
            _service.Setup(s => s.UpdateAsync(modelMock)).Throws(new Exception());

            var response = await _controller.Update(modelMock.Id.ToString(), modelMock);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestDelete()
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            _service.Setup(s => s.DeleteAsync(id)).Returns(Task.CompletedTask);

            var response = await _controller.Delete(id);

            Assert.Equal(204, response.StatusCode);
        }

        [Fact]
        public async void TestDelete404()
        {
            var id = "";
            _service.Setup(s => s.DeleteAsync(id)).Throws(new NotFoundException());

            var response = await _controller.Delete(id);

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async void TestDelete500()
        {
            var id = "";
            _service.Setup(s => s.DeleteAsync(id)).Throws(new Exception());

            var response = await _controller.Delete(id);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void CreatePdf()
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var modelMock = new Schedule()
                { Id = guid, User = new User(), Vehicle = new Vehicle() };

            _service.Setup(s => s.FindAsync(
                    id, include => include.User, include => include.Vehicle,
                    include => include.Vehicle.VehicleBrand,
                    include => include.Vehicle.VehicleModel,
                    include => include.Vehicle.VehicleCategory
                ))
                .Returns(Task.FromResult(modelMock));

            _nodeService.Setup(s => s.InvokeAsync<byte[]>("./pdf"))
                .Returns(Task.FromResult(Array.Empty<byte>()));

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            await _controller.CreatePdf(id, _nodeService.Object);

            Assert.Equal("application/pdf", _controller.HttpContext.Response.ContentType);
            Assert.Equal(200, _controller.HttpContext.Response.StatusCode);
            Assert.Equal(
                "Contrato de Locacao.pdf",
                _controller.HttpContext.Response.Headers["x-filename"]
            );
            Assert.Equal(
                "x-filename",
                _controller.HttpContext.Response.Headers["Access-Control-Expose-Headers"]
            );
        }

        [Fact]
        public async void CreatePdf404()
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();

            _service.Setup(s => s.FindAsync(
                    id, include => include.User, include => include.Vehicle,
                    include => include.Vehicle.VehicleBrand,
                    include => include.Vehicle.VehicleModel,
                    include => include.Vehicle.VehicleCategory
                ))
                .Throws(new NotFoundException());

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            await _controller.CreatePdf(id, _nodeService.Object);

            Assert.Equal(404, _controller.HttpContext.Response.StatusCode);
        }

        [Fact]
        public async void CreatePdf500()
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();

            _service.Setup(s => s.FindAsync(
                    id, include => include.User, include => include.Vehicle,
                    include => include.Vehicle.VehicleBrand,
                    include => include.Vehicle.VehicleModel,
                    include => include.Vehicle.VehicleCategory
                ))
                .Throws(new Exception());

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            await _controller.CreatePdf(id, _nodeService.Object);

            Assert.Equal(500, _controller.HttpContext.Response.StatusCode);
        }
    }
}
