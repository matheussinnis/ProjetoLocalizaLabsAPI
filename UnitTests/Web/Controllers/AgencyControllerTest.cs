using System;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Database.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web.Controllers;
using Xunit;

namespace UnitTests.Web.Controllers
{
    public class AgencyControllerTest
    {
        private AgencyController _controller { get; set; }
        private Mock<IAgencyService> _service { get; set; }

        public AgencyControllerTest()
        {
            var logger = new Mock<ILogger<Agency>>();
            _service = new Mock<IAgencyService>();
            _controller = new AgencyController(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetAll()
        {
            _service.Setup(s => s.GetAllAsync()).Returns(
                Task.FromResult(Enumerable.Empty<Agency>())
            );

            var response = await _controller.GetAll();

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<Agency>(), response.Value);
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
            var modelMock = new Agency() { Id = guid };
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
            var modelMock = new Agency() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Create(modelMock);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestCreate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Agency() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Throws(new Exception());

            var response = await _controller.Create(modelMock);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestUpdate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Agency() { Id = guid };
            _service.Setup(s => s.UpdateAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Update(modelMock.Id.ToString(), modelMock);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestUpdate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new Agency() { Id = guid };
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void TestGetVehicles(bool available)
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var withdrawalDate = DateTime.Now;
            var page = 1;
            var perPage = 10;
            var vehicles = Enumerable.Empty<AvailableVehicleDto>();
            _service.Setup(s => s.GetVehicles(id, available, withdrawalDate, page, perPage))
                .Returns(Task.FromResult(vehicles));

            var response = await _controller.GetVehicles(
                id, available, withdrawalDate, page, perPage
            );

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(vehicles, ((ObjectResult) response).Value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async void TestGetVehicles500(bool available)
        {
            var guid = Guid.NewGuid();
            var id = guid.ToString();
            var withdrawalDate = DateTime.Now;
            var page = 1;
            var perPage = 10;
            _service.Setup(s => s.GetVehicles(id, available, withdrawalDate, page, perPage))
                .Throws(new Exception());

            var response = await _controller.GetVehicles(
                id, available, withdrawalDate, page, perPage
            );

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void GetNearestAgencies()
        {
            var latitude = 1;
            var longitude = 10;
            _service.Setup(s => s.GetNearestAgencies(latitude, longitude))
                .Returns(Task.FromResult(Enumerable.Empty<dynamic>()));

            var response = await _controller.GetNearestAgencies(latitude, longitude);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<dynamic>(), ((ObjectResult) response).Value);
        }

        [Fact]
        public async void GetNearestAgencies500()
        {
            var latitude = 1;
            var longitude = 10;
            _service.Setup(s => s.GetNearestAgencies(latitude, longitude))
                .Throws(new Exception());

            var response = await _controller.GetNearestAgencies(latitude, longitude);

            Assert.Equal(500, response.StatusCode);
        }
    }
}
