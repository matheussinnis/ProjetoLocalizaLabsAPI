using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.NodeServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScheduleController : CrudController<Schedule>
    {
        public ScheduleController(ILogger<Schedule> logger, IScheduleService service)
            : base(logger, service) {}

        private static object SerializeAndDeserializeIgnoringCyclicDependencies(object obj)
        {
            return JsonConvert.DeserializeObject(
                JsonConvert.SerializeObject(
                    obj, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }
                )
            );
        }

        [HttpGet("{id}.pdf")]
        [Authorize]
        public async Task CreatePdf(string id, [FromServices] INodeServices nodeServices)
        {
            try
            {
                var entity = await _service.FindAsync(
                    id, include => include.User, include => include.Vehicle,
                    include => include.Vehicle.VehicleBrand,
                    include => include.Vehicle.VehicleModel,
                    include => include.Vehicle.VehicleCategory
                );
                var result = await nodeServices.InvokeAsync<byte[]>(
                    "./pdf",
                    SerializeAndDeserializeIgnoringCyclicDependencies(entity.User),
                    SerializeAndDeserializeIgnoringCyclicDependencies(entity.Vehicle),
                    SerializeAndDeserializeIgnoringCyclicDependencies(entity)
                );

                HttpContext.Response.ContentType = "application/pdf";
                HttpContext.Response.Headers.Add("x-filename", "Contrato de Locacao.pdf");
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "x-filename");
                HttpContext.Response.StatusCode = 200;
                await HttpContext.Response.Body.WriteAsync(result, 0, result.Length);
            }

            catch (NotFoundException exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                HttpContext.Response.StatusCode = 404;
                var obj = JsonConvert.SerializeObject(new {Message = exception.Message});
                var objBytes = System.Text.Encoding.UTF8.GetBytes(obj);
                await HttpContext.Response.Body.WriteAsync(objBytes, 0, objBytes.Length);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                HttpContext.Response.StatusCode = 500;
                var obj = JsonConvert.SerializeObject(new {Message = exception.Message});
                var objBytes = System.Text.Encoding.UTF8.GetBytes(obj);
                await HttpContext.Response.Body.WriteAsync(objBytes, 0, objBytes.Length);
            }
        }
    }
}
