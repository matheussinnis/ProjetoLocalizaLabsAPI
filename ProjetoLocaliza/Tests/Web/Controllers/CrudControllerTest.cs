using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Tests.Mocks;
using Web.Controllers;
using Xunit;

namespace Tests.Web.Controllers
{
    public class CrudControllerTest
    {
        public CrudControllerMock _controller { get; set; }
        public Mock<IBaseService<ModelMock>> _service { get; set; }

        public CrudControllerTest()
        {
            var logger = new Mock<ILogger<ModelMock>>();
            _service = new Mock<IBaseService<ModelMock>>();
            _controller = new CrudControllerMock(logger.Object, _service.Object);
        }

        [Fact]
        public async void TestGetAll()
        {
            _service.Setup(s => s.GetAllAsync()).Returns(
                Task.FromResult(Enumerable.Empty<ModelMock>())
            );

            var response = await _controller.GetAll();

            Assert.Equal(200, response.StatusCode);
            Assert.Equal(Enumerable.Empty<ModelMock>(), response.Value);
        }
    }
}
