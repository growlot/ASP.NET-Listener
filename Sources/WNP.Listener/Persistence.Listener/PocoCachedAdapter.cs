// //-----------------------------------------------------------------------
// // <copyright file="PocoCachedAdapter.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Persistence.Listener
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using AsyncPoco;
    using Core;

    public class PocoCachedAdapter : IPersistenceAdapter
    {
        private static readonly ConcurrentDictionary<string, object> _innerCache =
            new ConcurrentDictionary<string, object>();

        private readonly ListenerDbContext _dbContext;

        private readonly SemaphoreSlim cacheLock = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoCachedAdapter"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PocoCachedAdapter(ListenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<TEntity>> GetListAsync<TEntity>(string query, params object[] args)
        {
            return ReadOrAdd((db, a) => a == null ? db.FetchAsync<TEntity>(query) : db.FetchAsync<TEntity>(query, a),
                args);
        }

        public Task<TEntity> GetAsync<TEntity>(string query, params object[] args)
        {
            return ReadOrAdd((db, a) => a == null ? db.SingleAsync<TEntity>(query) : db.SingleAsync<TEntity>(query, a),
                args);
        }

        public Task UpdateAsync<TEntity>(TEntity entity)
        {
            return _dbContext.UpdateAsync(entity);
        }

        public Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> columnsToUpdate)
        {
            var tableNameAttribute = entity.GetType().GetCustomAttribute<TableNameAttribute>();
            var primaryColumnAttribute = entity.GetType().GetCustomAttribute<PrimaryKeyAttribute>();

            return
                _dbContext.UpdateAsync(tableNameAttribute.Value, primaryColumnAttribute.Value, entity, columnsToUpdate);
        }

        public Task InsertAsync<TEntity>(TEntity entity)
        {
            return _dbContext.InsertAsync(entity);
        }

        public Task<TValue> ExecuteScalarAsync<TValue>(string query, params object[] args)
        {
            return _dbContext.ExecuteScalarAsync<TValue>(query, args);
        }

        public Task<List<T2>> ProjectionAsync<T, T1, T2>(Func<T, T1, T2> func, string query, params object[] args)
        {
            return _dbContext.FetchAsync(func, query, args);
        }

        public Task<List<T4>> ProjectionAsync<T, T1, T2, T3, T4>(Func<T, T1, T2, T3, T4> func, string query,
            params object[] args)
        {
            return _dbContext.FetchAsync<T4>(new[] { typeof (T), typeof (T1), typeof (T2), typeof (T3) },
                func, query, args);
        }

        private async Task<TEntity> ReadOrAdd<TEntity>(Func<ListenerDbContext, object[], Task<TEntity>> selector,
            params object[] args)
        {
            string key = $"{typeof (TEntity)}_{string.Join("_", args)}";

            await cacheLock.WaitAsync();
            try
            {
                object result;
                if (_innerCache.TryGetValue(key, out result))
                {
                    return (TEntity) result;
                }
                else
                {
                    var returnValue = await selector(_dbContext, args);
                    _innerCache.TryAdd(key, returnValue);
                    return returnValue;
                }
            }
            finally
            {
                cacheLock.Release();
            }
        }
    }
}