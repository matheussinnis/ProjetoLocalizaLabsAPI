using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Interfaces
{
    public interface ICrudRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        ICrudRepository<T> Skip(int skip);
        ICrudRepository<T> Limit(int limit);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes
        );

        async Task<T> FindAsync(string id, params Expression<Func<T, object>>[] includes) => (
                await FilterAsync(entity => entity.Id.ToString() == id, includes)
            ).FirstOrDefault();
    }
}
