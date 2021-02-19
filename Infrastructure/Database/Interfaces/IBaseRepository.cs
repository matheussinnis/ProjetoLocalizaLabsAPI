using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> SaveChangesAsync();

        Task<IDisposable> BeginTransaction();

        Task Commit();

        Task Rollback();

        Task<IEnumerable<dynamic>> CollectionFromSql(FormattableString sql);

        Task<List<T>> FromSqlInterpolated(FormattableString sql);
    }
}
