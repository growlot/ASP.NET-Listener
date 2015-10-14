using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Core
{
    using System.Threading.Tasks;

    public interface IPersistenceAdapter
    {
        Task<List<TEntity>> GetListAsync<TEntity>(string query, params object[] args);
        Task<TEntity> GetAsync<TEntity>(string query, params object[] args);
        Task UpdateAsync<TEntity>(TEntity entity);
        Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue);
        Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> columnsToUpdate);
        Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue, IEnumerable<string> columnsToUpdate);
        Task InsertAsync<TEntity>(TEntity entity);
        Task<TValue> ExecuteScalarAsync<TValue>(string query, params object[] args);
        Task<List<T2>> ProjectionAsync<T, T1, T2>(Func<T, T1, T2> func, string query, params object[] args);
        Task<List<T4>> ProjectionAsync<T, T1, T2, T3, T4>(Func<T, T1, T2, T3, T4> func, string query, params object[] args);
    }
}
