using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Interfaces;
using Infrastructure.Database.Interfaces;

namespace Domain.Services
{
    public class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository) => this._repository = repository;

        public async Task<T> AddAsync(T t)
        {
            await _repository.AddAsync(t);
            await _repository.SaveChangesAsync();
            return t;
        }

        public async Task<T> UpdateAsync(T t)
        {
            _repository.Update(t);
            await _repository.SaveChangesAsync();
            return t;
        }

        public async Task DeleteAsync(T t)
        {
            _repository.Delete(t);
            await _repository.SaveChangesAsync();
        }

        public Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] expressions)
        {
            return _repository.GetAllAsync(expressions);
        }

        public Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where = null,
            params Expression<Func<T, object>>[] expressions
        )
        {
            return _repository.FilterAsync(where, expressions);
        }
    }
}
