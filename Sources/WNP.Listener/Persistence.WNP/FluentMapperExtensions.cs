// <copyright file="FluentMapperExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNP
{
    using System;
    using System.Linq.Expressions;
    using AsyncPoco;

    /// <summary>
    /// Custom extensions for <see cref="FluentMapper{T}"/>
    /// </summary>
    public static class FluentMapperExtensions
    {
        /// <summary>
        /// Properties the specified action.
        /// </summary>
        /// <typeparam name="T">The type of POCO.</typeparam>
        /// <typeparam name="P">The type of POCO property.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        /// <param name="column">The column.</param>
        /// <param name="primaryKey">if set to <c>true</c> [primary key].</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>The updated fluent mapper</returns>
        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, bool primaryKey = false, bool readOnly = false)
            where T : class
        {
            return obj.Property(action, column, null, primaryKey, readOnly);
        }

        /// <summary>
        /// Properties the specified action.
        /// </summary>
        /// <typeparam name="T">The type of POCO.</typeparam>
        /// <typeparam name="P">The type of POCO property.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        /// <param name="column">The column.</param>
        /// <param name="fromDbConverter">From database converter.</param>
        /// <param name="primaryKey">if set to <c>true</c> [primary key].</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>The updated fluent mapper</returns>
        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, Func<object, object> fromDbConverter, bool primaryKey = false, bool readOnly = false)
            where T : class
        {
            return obj.Property(action, column, fromDbConverter, null, primaryKey, readOnly);
        }

        /// <summary>
        /// Properties the specified object.
        /// </summary>
        /// <typeparam name="T">The type of POCO.</typeparam>
        /// <typeparam name="P">The type of POCO property.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="action">The action.</param>
        /// <param name="column">The column.</param>
        /// <param name="fromDbConverter">From database converter.</param>
        /// <param name="toDbConverter">To database converter.</param>
        /// <param name="primaryKey">if set to <c>true</c> [primary key].</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        /// <returns>The updated fluent mapper</returns>
        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, Func<object, object> fromDbConverter, Func<object, object> toDbConverter, bool primaryKey = false, bool readOnly = false)
                    where T : class
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            var columnInfo = new ColumnInfo()
            {
                ColumnName = column,
                ResultColumn = readOnly
            };
            var columnMap = new FluentColumnMap(columnInfo, fromDbConverter, toDbConverter);
            obj.AddMapping(name, columnMap);

            if (primaryKey)
            {
                if (!string.IsNullOrEmpty(obj.GetTableInfo(typeof(P)).PrimaryKey))
                {
                    obj.GetTableInfo(typeof(P)).PrimaryKey += ",";
                }

                obj.GetTableInfo(typeof(P)).PrimaryKey += column;
            }

            return obj;
        }
    }
}