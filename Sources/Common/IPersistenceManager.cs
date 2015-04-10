//-----------------------------------------------------------------------
// <copyright file="IPersistenceManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NHibernate.Criterion;

    /// <summary>
    /// Interface for implementing CRUD for data entities.
    /// </summary>
    public interface IPersistenceManager : IDisposable
    {
        /// <summary>
        /// Gets the database type.
        /// </summary>
        /// <value>
        /// The database (ex. MSSQL2005, Oracle, etc.)
        /// </value>
        string Database { get; }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        string ConnectionString { get; }

        /// <summary>
        /// Retrieves all objects of a specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="sessionAction">The session action.</param>
        /// <returns>
        /// A list of all objects of the specified type.
        /// </returns>
        IList<T> RetrieveAll<T>(SessionAction sessionAction);

        /// <summary>
        /// Retrieves all objects of a specified type meeting specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="selectCriteria">The selection criteria.</param>
        /// <returns>
        /// A list of all objects of the specified type meeting specified criteria.
        /// </returns>
        IList<T> RetrieveAllEqual<T>(DetachedCriteria selectCriteria);

        /// <summary>
        /// Retrieves unique object of a specified type meeting specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="selectCriteria">The selection criteria.</param>
        /// <returns>
        /// A unique objects of the specified type meeting specified criteria.
        /// </returns>
        T RetrieveUnique<T>(DetachedCriteria selectCriteria);

        /// <summary>
        /// Retrieves first object of a specified type meeting specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of objects to retrieve.</typeparam>
        /// <param name="selectCriteria">The selection criteria.</param>
        /// <returns>
        /// First object of the specified type meeting specified criteria.
        /// </returns>
        T RetrieveFirstEqual<T>(DetachedCriteria selectCriteria);
        
        /// <summary>
        /// Retrieves first object of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>
        /// First object meeting the specified criteria.
        /// </returns>
        T RetrieveFirstEqual<T>(string propertyName, object propertyValue);

        /// <summary>
        /// Gets the object by primary key.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="key">The primary key.</param>
        /// <returns>Object represented by primary key.</returns>
        T GetByKey<T>(object key);

        /// <summary>
        /// Gets the object by primary key.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="key">The primary key.</param>
        /// <param name="lazy">If set to <c>true</c> [lazy] uses nHibernate Load method. If set to <c>false</c> uses Get method.</param>
        /// <returns>Object represented by primary key.</returns>
        T GetByKey<T>(object key, bool lazy);

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be saved.</typeparam>
        /// <param name="item">The item.</param>
        void Save<T>(T item);

        /// <summary>
        /// Saves all objects and their persistent children.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be saved.</typeparam>
        /// <param name="items">The list of items.</param>
        void SaveBulk<T>(IEnumerable<T> items);

        /// <summary>
        /// Deletes all persisted items of specified type.
        /// </summary>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "False positive")]
        void DeleteAll<T>();

        /// <summary>
        /// Deletes the specified item.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="id">The identifier.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "False positive")]
        void Delete<T>(int id);
    }
}
