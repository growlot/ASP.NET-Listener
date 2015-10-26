// <copyright file="IPersistenceAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface to abstract persistence implementation.
    /// </summary>
    public interface IPersistenceAdapter
    {
        /// <summary>
        /// Gets the list of entities asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>The list of entities.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "None of the parameters can be supplied with correct type.")]
        Task<List<TEntity>> GetListAsync<TEntity>(string query, params object[] args);

        /// <summary>
        /// Gets the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>The entity.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "None of the parameters can be supplied with correct type.")]
        Task<TEntity> GetAsync<TEntity>(string query, params object[] args);

        /// <summary>
        /// Updates the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The updated entity.</param>
        /// <returns>The empty task.</returns>
        Task UpdateAsync<TEntity>(TEntity entity);

        /// <summary>
        /// Updates the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entity">The updated entity without primary key information.</param>
        /// <param name="primaryKeyValue">The primary key.</param>
        /// <returns>The empty task.</returns>
        Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue);

        /// <summary>
        /// Updates the entity asynchronously. Only updates specified columns.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The updated entity.</param>
        /// <param name="columnsToUpdate">The list of columns to update.</param>
        /// <returns>The empty task.</returns>
        Task UpdateAsync<TEntity>(TEntity entity, IEnumerable<string> columnsToUpdate);

        /// <summary>
        /// Updates the entity asynchronously. Only updates specified columns.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="entity">The updated entity without primary key information.</param>
        /// <param name="primaryKeyValue">The primary key value.</param>
        /// <param name="columnsToUpdate">The columns to update.</param>
        /// <returns>The empty task.</returns>
        Task UpdateAsync<TEntity, TKey>(TEntity entity, TKey primaryKeyValue, IEnumerable<string> columnsToUpdate);

        /// <summary>
        /// Inserts the entity asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The empty task.</returns>
        Task InsertAsync<TEntity>(TEntity entity);

        /// <summary>
        /// Executes the scalar operation asynchronously.
        /// </summary>
        /// <typeparam name="TValue">The type of the result value.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>The result of scalar operation.</returns>
        Task<TValue> ExecuteScalarAsync<TValue>(string query, params object[] args);

        /// <summary>
        /// Projects retrieved entities to return object.
        /// </summary>
        /// <typeparam name="T1">The first enity type</typeparam>
        /// <typeparam name="T2">The second enity type</typeparam>
        /// <typeparam name="TRet">The type of the return object.</typeparam>
        /// <param name="func">The function that maps entities to return type.</param>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>
        /// The list of retrun objects.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is expected design.")]
        Task<List<TRet>> ProjectionAsync<T1, T2, TRet>(Func<T1, T2, TRet> func, string query, params object[] args);

        /// <summary>
        /// Projects retrieved entities to return object.
        /// </summary>
        /// <typeparam name="T1">The first enity type</typeparam>
        /// <typeparam name="T2">The second enity type</typeparam>
        /// <typeparam name="T3">The third entity type.</typeparam>
        /// <typeparam name="TRet">The type of the return object.</typeparam>
        /// <param name="func">The function that maps entities to return type.</param>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>
        /// The list of retrun objects.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is expected design.")]
        Task<List<TRet>> ProjectionAsync<T1, T2, T3, TRet>(Func<T1, T2, T3, TRet> func, string query, params object[] args);

        /// <summary>
        /// Projects retrieved entities to return object.
        /// </summary>
        /// <typeparam name="T1">The first enity type</typeparam>
        /// <typeparam name="T2">The second enity type</typeparam>
        /// <typeparam name="T3">The third entity type.</typeparam>
        /// <typeparam name="T4">The forth entity type.</typeparam>
        /// <typeparam name="TRet">The type of the return object.</typeparam>
        /// <param name="func">The function that maps entities to return type.</param>
        /// <param name="query">The query.</param>
        /// <param name="args">The query arguments.</param>
        /// <returns>
        /// The list of retrun objects.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is expected design.")]
        Task<List<TRet>> ProjectionAsync<T1, T2, T3, T4, TRet>(Func<T1, T2, T3, T4, TRet> func, string query, params object[] args);
    }
}
