using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Database.Interfaces;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Database.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DataContext _context;
        protected IDbContextTransaction _transaction { get; set; }

        public BaseRepository(DataContext context) => _context = context;

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IDisposable> BeginTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public Task Commit()
        {
            return _transaction.CommitAsync();
        }

        public Task Rollback()
        {
            return _transaction.RollbackAsync();
        }

        private dynamic GetDataRow(DbDataReader dataReader)
        {
            var dataRow = new ExpandoObject() as IDictionary<string, object>;
            for (var i = 0; i < dataReader.FieldCount; i++)
                dataRow.Add(dataReader.GetName(i), dataReader[i]);
            return dataRow;
        }

        private string MountQuery(FormattableString sql)
        {
            var query = sql.Format;
            for (int i = 0; i < sql.ArgumentCount; i++)
                query = query.Replace($"{{{i}}}", $"@v{i}");
            return query;
        }

        public async Task<IEnumerable<dynamic>> CollectionFromSql(FormattableString sql)
        {
            var list = new List<dynamic>();
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = MountQuery(sql);
                if (cmd.Connection.State != ConnectionState.Open)
                    await cmd.Connection.OpenAsync();

                var args = sql.GetArguments();
                for (int i = 0; i < sql.ArgumentCount; i++)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = $"@v{i}";
                    dbParameter.Value = args[i];
                    cmd.Parameters.Add(dbParameter);
                }

                await using (var dataReader = await cmd.ExecuteReaderAsync())
                    while (await dataReader.ReadAsync())
                        list.Add(GetDataRow(dataReader));
            }
            return list;
        }

        public Task<List<T>> FromSqlInterpolated(FormattableString sql)
        {
            return _context.Set<T>().FromSqlInterpolated(sql).ToListAsync();
        }
    }
}
