using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext _context;

        public BaseRepository(DataContext context) => _context = context;

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

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var expression in includes)
            {
                query = query.Include(expression);
            }
            return await query.ToListAsync();
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
            return await query.ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<IDbContextTransaction> BeginTransaction()
        {
            return _context.Database.BeginTransactionAsync();
        }
    }
}
