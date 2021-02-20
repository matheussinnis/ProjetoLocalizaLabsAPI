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
    public class UserControllerTest
    {
        private UserController _controller { get; set; }
        private Mock<IUserService> _service { get; set; }

        public UserControllerTest()
        {
            var logger = new Mock<ILogger<User>>();
            _service = new Mock<IUserService>();
            _controller = new UserController(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetAll()
        {
            _service.Setup(s => s.GetAllAsync()).Returns(
                Task.FromResult(Enumerable.Empty<User>())
            );

            var response = await _controller.GetAll();

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<User>(), response.Value);
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
            var modelMock = new User() { Id = guid };
            _service.Setup(s => s.FindAsync(id, i => i.Address)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Find(id);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestFind404()
        {
            var id = "";
            _service.Setup(s => s.FindAsync(id, i => i.Address)).Throws(new NotFoundException());

            var response = await _controller.Find(id);

            Assert.Equal(404, response.StatusCode);
        }

        [Fact]
        public async void TestFind500()
        {
            var id = "";
            _service.Setup(s => s.FindAsync(id, i => i.Address)).Throws(new Exception());

            var response = await _controller.Find(id);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestCreate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new User() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Create(modelMock);

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestCreate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new User() { Id = guid };
            _service.Setup(s => s.AddAsync(modelMock)).Throws(new Exception());

            var response = await _controller.Create(modelMock);

            Assert.Equal(500, response.StatusCode);
        }

        [Fact]
        public async void TestUpdate()
        {
            var guid = Guid.NewGuid();
            var modelMock = new User() { Id = guid };
            _service.Setup(s => s.UpdateAsync(modelMock)).Returns(Task.FromResult(modelMock));

            var response = await _controller.Update(modelMock.Id.ToString(), modelMock);

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(modelMock, response.Value);
        }

        [Fact]
        public async void TestUpdate500()
        {
            var guid = Guid.NewGuid();
            var modelMock = new User() { Id = guid };
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
