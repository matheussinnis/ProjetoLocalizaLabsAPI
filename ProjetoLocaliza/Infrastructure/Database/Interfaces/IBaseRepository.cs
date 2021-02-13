using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] expressions);
        Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] expressions
        );

        Task<int> SaveChangesAsync();

        Task<IDbContextTransaction> BeginTransaction();
    }
}
