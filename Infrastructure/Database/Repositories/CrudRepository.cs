using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;

namespace Infrastructure.Database.Repositories
{
    public class CrudRepository<T> : BaseRepository<T>, ICrudRepository<T> where T : BaseEntity
    {
        private int _skip = -1;
        private int _limit = -1;

        public CrudRepository(DataContext context) : base(context) {}

        public ICrudRepository<T> Skip(int skip)
        {
            _skip = skip;
            return this;
        }

        public ICrudRepository<T> Limit(int limit)
        {
            _limit = limit;
            return this;
        }

        private void ResetFields()
        {
            _skip = -1;
            _limit = -1;
        }

        private IQueryable<T> IncrementQuery(IQueryable<T> query)
        {
            if (_skip != -1) query = query.Skip(_skip);
            if (_limit != -1) query = query.Take(_limit);
            ResetFields();
            return query;
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            params Expression<Func<T, object>>[] includes
        )
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var expression in includes)
            {
                query = query.Include(expression);
            }
            return await IncrementQuery(query).ToListAsync();
        }

        public async Task<IEnumerable<T>> FilterAsync(
            Expression<Func<T, bool>> where,
            params Expression<Func<T, object>>[] includes
        )
        {
            var query = _context.Set<T>().Where(where);
            foreach (var expression in includes)
            {
                query = query.Include(expression);
            }
            return await IncrementQuery(query).ToListAsync();
        }
    }
}
