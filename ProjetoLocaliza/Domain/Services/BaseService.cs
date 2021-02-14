using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository) => this._repository = repository;

        public virtual async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T t)
        {
            _repository.Update(t);
            await _repository.SaveChangesAsync();
            return t;
        }

        public virtual async Task DeleteAsync(string id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} não encontrado(a)");

            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            return _repository.GetAllAsync(includes);
        }

        public virtual Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where = null,
            params Expression<Func<T, object>>[] includes
        )
        {
            return _repository.FilterAsync(where, includes);
        }

        public virtual async Task<T> FindAsync(string id)
        {
            var entity = await _repository.FindAsync(id);
            if (entity == null)
                throw new NotFoundException($"{typeof(T).Name} não encontrado(a)");

            return entity;
        }
    }
}
