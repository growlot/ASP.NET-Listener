﻿//-----------------------------------------------------------------------
// <copyright file="PocoCachedAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Persistence.Poco
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Repository;
    using AsyncPoco;

    public class PocoCachedAdapter : IPersistenceAdapter
    {
        private static readonly ConcurrentDictionary<string, object> _innerCache =
            new ConcurrentDictionary<string, object>();

        private readonly Database _dbContext;

        private readonly SemaphoreSlim cacheLock = new SemaphoreSlim(1);

        /// <summary>
        /// Initializes a new instance of the <see cref="PocoCachedAdapter"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public PocoCachedAdapter(Database dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Begins the transaction.
        /// </summary>
        /// <returns>Task&lt;ITransaction&gt;.</returns>
        public async Task<ITransactionProxy> BeginTransaction()
        {
            return new TransactionProxy(await this._dbContext.GetTransactionAsync());
        }

        public Task<List<TEntity>> GetListAsync<TEntity>(string query, params object[] args)
        {
            return this.ReadOrAdd<List<TEntity>>((db, a) => a == null ? db.FetchAsync<TEntity>(query) : db.FetchAsync<TEntity>(query, a),
                args);
        }

        public Task<List<TEntity>> GetListAsync<TEntity>(string query, bool useCache, params object[] args)
        {
            return useCache ? this.GetListAsync<TEntity>(query, args) : args == null ? this._dbContext.FetchAsync<TEntity>(query) : this._dbContext.FetchAsync<TEntity>(query, args);
        }

        public Task<TEntity> GetAsync<TEntity>(string query, params object[] args)
        {
            return this.ReadOrAdd<TEntity>((db, a) => a == null ? db.SingleAsync<TEntity>(query) : db.SingleAsync<TEntity>(query, a),
                args);
        }

        public Task UpdateAsync<TEntity>(TEntity entity)
        {
            return this._dbContext.UpdateAsync(entity);
        }

        public Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue)
        {
            return this._dbContext.UpdateAsync(entity, primaryKeyValue);
        }

        public Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> columnsToUpdate)
        {
            var tableNameAttribute = entity.GetType().GetCustomAttribute<TableNameAttribute>();
            var primaryColumnAttribute = entity.GetType().GetCustomAttribute<PrimaryKeyAttribute>();

            return
                this._dbContext.UpdateAsync(tableNameAttribute.Value, primaryColumnAttribute.Value, entity, columnsToUpdate);
        }

        public Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue, IEnumerable<string> columnsToUpdate)
        {
            //var tableNameAttribute = entity.GetType().GetCustomAttribute<TableNameAttribute>();
            //var primaryColumnAttribute = entity.GetType().GetCustomAttribute<PrimaryKeyAttribute>();

            return
                this._dbContext.UpdateAsync(entity, primaryKeyValue, columnsToUpdate);
        }

        public Task InsertAsync<TEntity>(TEntity entity)
        {
            return this._dbContext.InsertAsync(entity);
        }

        public Task InsertAsync<TEntity>(TEntity entity, string tableName, string primaryColumnName)
        {
            return this._dbContext.InsertAsync(tableName, primaryColumnName, entity);
        }

        public Task<TValue> ExecuteScalarAsync<TValue>(string query, params object[] args)
        {
            return this._dbContext.ExecuteScalarAsync<TValue>(query, args);
        }

        public Task<List<T2>> ProjectionAsync<T, T1, T2>(Func<T, T1, T2> func, string query, params object[] args)
        {
            return this._dbContext.FetchAsync(func, query, args);
        }

        public Task<List<T3>> ProjectionAsync<T, T1, T2, T3>(Func<T, T1, T2, T3> func, string query, params object[] args)
        {
            return this._dbContext.FetchAsync<T3>(new[] { typeof(T), typeof(T1), typeof(T2) },
                func, query, args);
        }

        public Task<List<T4>> ProjectionAsync<T, T1, T2, T3, T4>(Func<T, T1, T2, T3, T4> func, string query,
            params object[] args)
        {
            return this._dbContext.FetchAsync<T4>(new[] { typeof(T), typeof(T1), typeof(T2), typeof(T3) },
                func, query, args);
        }

        public Task<List<T5>> ProjectionAsync<T, T1, T2, T3, T4, T5>(Func<T, T1, T2, T3, T4, T5> func, string query,
                    params object[] args)
        {
            return this._dbContext.FetchAsync<T5>(new[] { typeof(T), typeof(T1), typeof(T2), typeof(T3), typeof(T4) },
                func, query, args);
        }

        private async Task<TEntity> ReadOrAdd<TEntity>(Func<Database, object[], Task<TEntity>> selector,
            params object[] args)
        {
            string key = $"{typeof(TEntity)}_{string.Join("_", args)}";

            await this.cacheLock.WaitAsync();
            try
            {
                object result;
                if (_innerCache.TryGetValue(key, out result))
                {
                    return (TEntity)result;
                }
                else
                {
                    var returnValue = await selector(this._dbContext, args);
                    _innerCache.TryAdd(key, returnValue);
                    return returnValue;
                }
            }
            finally
            {
                this.cacheLock.Release();
            }
        }
    }
}