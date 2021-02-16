using Core.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Web.Controllers;

namespace Tests.Mocks
{
    public class CrudControllerMock : CrudController<ModelMock>
    {
        public CrudControllerMock(
            ILogger<ModelMock> logger,
            IBaseService<ModelMock> service
        ) : base(logger, service)
        {
        }
    }
}
