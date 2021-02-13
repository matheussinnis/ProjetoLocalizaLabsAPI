using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBaseService<T>
    {
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] expressions);
        Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] expressions
        );
    }
}
