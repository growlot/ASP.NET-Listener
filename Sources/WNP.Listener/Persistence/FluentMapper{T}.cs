// <copyright file="FluentMapper{T}.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Implments custom PetaPoco's Database to POCO mapping mechanism.
    /// </summary>
    /// <typeparam name="T">The type of POCO.</typeparam>
    public abstract class FluentMapper<T> : IMapper
    {
        private Dictionary<string, FluentColumnMap> mappings = new Dictionary<string, FluentColumnMap>();
        private TableInfo tableInfo = new TableInfo();

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentMapper{T}" /> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        protected FluentMapper(string tableName)
        {
            this.tableInfo.TableName = tableName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FluentMapper{T}"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="primaryKey">The primary key.</param>
        protected FluentMapper(string tableName, string primaryKey)
        {
            this.tableInfo.TableName = tableName;
            this.tableInfo.PrimaryKey = primaryKey;
        }

        /// <summary>
        /// Gets the table information.
        /// </summary>
        /// <param name="pocoType">Type of the poco.</param>
        /// <returns>The table information</returns>
        public TableInfo GetTableInfo(Type pocoType)
        {
            return this.tableInfo;
        }

        /// <summary>
        /// Adds the mapping.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="columnMap">The column map.</param>
        public void AddMapping(string name, FluentColumnMap columnMap)
        {
            this.mappings.Add(name, columnMap);
        }

        /// <inheritdoc/>
        public ColumnInfo GetColumnInfo(PropertyInfo pocoProperty)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.mappings.TryGetValue(pocoProperty.Name, out fluentMap))
            {
                return fluentMap.ColumnInfo;
            }

            return null;
        }

        /// <inheritdoc/>
        public Func<object, object> GetFromDbConverter(PropertyInfo TargetProperty, Type SourceType)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.mappings.TryGetValue(TargetProperty.Name, out fluentMap))
            {
                return fluentMap.FromDbConverter;
            }

            return null;
        }

        /// <inheritdoc/>
        public Func<object, object> GetToDbConverter(PropertyInfo SourceProperty)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.mappings.TryGetValue(SourceProperty.Name, out fluentMap))
            {
                return fluentMap.ToDbConverter;
            }

            return null;
        }
    }
}