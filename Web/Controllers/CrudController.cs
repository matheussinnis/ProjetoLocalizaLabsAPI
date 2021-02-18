using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class CrudController<T> : Controller where T : BaseEntity
    {
        protected readonly ILogger<T> _logger;
        protected readonly IBaseService<T> _service;

        public CrudController(ILogger<T> logger, IBaseService<T> service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public virtual async Task<ObjectResult> GetAll()
        {
            try
            {
                return StatusCode(200, await _service.GetAllAsync());
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public virtual async Task<ObjectResult> Find(string id)
        {
            try
            {
                var entity = await _service.FindAsync(id);
                return StatusCode(200, entity);
            }

            catch (NotFoundException exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(404, new{Message = exception.Message});
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpPost]
        [Authorize]
        public virtual async Task<ObjectResult> Create([FromBody] T entity)
        {
            try
            {
                var obj = await _service.AddAsync(entity);
                return StatusCode(201, obj);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public virtual async Task<ObjectResult> Update(string id, [FromBody] T entity)
        {
            try
            {
                entity.Id = new Guid(id);
                var obj = await _service.UpdateAsync(entity);
                return StatusCode(200, obj);
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public virtual async Task<IStatusCodeActionResult> Delete(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return StatusCode(204);
            }

            catch (NotFoundException exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(404, new{Message = exception.Message});
            }

            catch (Exception exception)
            {
                _logger.Log(LogLevel.Error, exception, exception.Message);
                return StatusCode(500, new{Message = exception.Message});
            }
        }
    }
}
