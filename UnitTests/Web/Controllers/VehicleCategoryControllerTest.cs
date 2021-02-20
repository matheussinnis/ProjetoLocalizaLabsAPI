using System;
using System.Collections.Generic;
using System.Linq;
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
    public class VehicleCategoryControllerTest
    {
        private VehicleCategoryController _controller { get; set; }
        private Mock<IBaseService<VehicleCategory>> _service { get; set; }

        public VehicleCategoryControllerTest()
        {
            var logger = new Mock<ILogger<VehicleCategory>>();
            _service = new Mock<IBaseService<VehicleCategory>>();
            _controller = new VehicleCategoryController(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetAll()
        {
            _service.Setup(s => s.GetAllAsync()).Returns(
                Task.FromResult(Enumerable.Empty<VehicleCategory>())
            );

            var httpContext = new DefaultHttpContext();
            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            var response = await _controller.GetAll();

            _service.Verify(s => s.GetAllAsync(), Times.Once);
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<VehicleCategory>(), response.Value);
        }

        [Fact]
        public async void TestGetAllWithName()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Query = new QueryCollection(
                new Dictionary<string, StringValues>()
                {
                    { "name", new StringValues("nameValue") }
                }
            );

            _service.Setup(s => s.FilterAsync(
                vehicleCategory => vehicleCategory.Name.Contains(
                    httpContext.Request.Query["name"]
                )
            )).Returns(Task.FromResult(Enumerable.Empty<VehicleCategory>()));

            _controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };
            var response = await _controller.GetAll();

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<VehicleCategory>(), response.Value);
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
            var modelMock = new VehicleCategory() { Id = guid };
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
            var modelMock = new VehicleCategory() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Create(modelMock);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestCreate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new VehicleCategory() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Throws(new Exception());

            var response = await _controller.Create(modelMock);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestUpdate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new VehicleCategory() { Id = guid };
            _service.Setup(s => s.UpdateAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Update(modelMock.Id.ToString(), modelMock);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestUpdate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new VehicleCategory() { Id = guid };
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
    }
}
