// <copyright file="AsyncMapper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.WNPAsync
{
    using System;
    using System.Reflection;
    using AsyncPoco;

    /// <summary>
    /// PetaPoco - AsyncPoco Mapper.
    /// </summary>
    public class AsyncMapper : IMapper
    {
        /// <summary>
        /// Constructs a TableInfo for a POCO by reading its attribute data
        /// </summary>
        /// <param name="pocoType">Type of the poco.</param>
        /// <returns>TableInfo.</returns>
        [CLSCompliant(false)]
        public TableInfo GetTableInfo(
            Type pocoType)
        {
            if (pocoType == null)
            {
                throw new ArgumentNullException(nameof(pocoType));
            }

            TableInfo ti = new TableInfo();

            // Get the table name
            var a = pocoType.GetCustomAttributes(typeof(WNP.TableNameAttribute), true);
            ti.TableName = a.Length == 0 ? pocoType.Name : (a[0] as WNP.TableNameAttribute).Value;

            // Get the primary key
            a = pocoType.GetCustomAttributes(typeof(WNP.PrimaryKeyAttribute), true);
            ti.PrimaryKey = a.Length == 0 ? "ID" : (a[0] as WNP.PrimaryKeyAttribute).Value;
            ti.SequenceName = a.Length == 0 ? null : (a[0] as WNP.PrimaryKeyAttribute).sequenceName;
            ti.AutoIncrement = a.Length == 0 ? false : (a[0] as WNP.PrimaryKeyAttribute).autoIncrement;

            return ti;
        }

        /// <summary>
        /// Constructs a ColumnInfo for a POCO property by reading its attribute data
        /// </summary>
        /// <param name="pocoProperty">The poco property.</param>
        /// <returns>ColumnInfo.</returns>
        [CLSCompliant(false)]
        public ColumnInfo GetColumnInfo(
            PropertyInfo pocoProperty)
        {
            if (pocoProperty == null)
            {
                throw new ArgumentNullException(nameof(pocoProperty));
            }

            // Check if declaring poco has [Explicit] attribute
            bool explicitColumns =
                pocoProperty.DeclaringType.GetCustomAttributes(typeof(WNP.ExplicitColumnsAttribute), true).Length > 0;

            // Check for [Column]/[Ignore] Attributes
            var colAttrs = pocoProperty.GetCustomAttributes(typeof(WNP.ColumnAttribute), true);
            if (explicitColumns)
            {
                if (colAttrs.Length == 0)
                {
                    return null;
                }
            }
            else
            {
                if (pocoProperty.GetCustomAttributes(typeof(WNP.IgnoreAttribute), true).Length != 0)
                {
                    return null;
                }
            }

            ColumnInfo ci = new ColumnInfo();

            // Read attribute
            if (colAttrs.Length > 0)
            {
                var colattr = (WNP.ColumnAttribute)colAttrs[0];

                ci.ColumnName = colattr.Name == null ? pocoProperty.Name : colattr.Name;
                ci.ForceToUtc = colattr.ForceToUtc;
                if ((colattr as WNP.ResultColumnAttribute) != null)
                {
                    ci.ResultColumn = true;
                }

                // if ((colattr as WNP.ComputedColumnAttribute) != null)
                // {
                //    ci.ComputedColumn = true;
                // }
            }
            else
            {
                ci.ColumnName = pocoProperty.Name;
                ci.ForceToUtc = false;
                ci.ResultColumn = false;

                // ci.ComputedColumn = false;
            }

            return ci;
        }

        /// <summary>
        /// Gets from database converter.
        /// </summary>
        /// <param name="TargetProperty">The target property.</param>
        /// <param name="SourceType">Type of the source.</param>
        /// <returns>Func&lt;System.Object, System.Object&gt;.</returns>
        [CLSCompliant(false)]
        public Func<object, object> GetFromDbConverter(
            PropertyInfo TargetProperty,
            Type SourceType)
        {
            return null;
        }

        /// <summary>
        /// Gets to database converter.
        /// </summary>
        /// <param name="SourceProperty">The source property.</param>
        /// <returns>Func&lt;System.Object, System.Object&gt;.</returns>
        [CLSCompliant(false)]
        public Func<object, object> GetToDbConverter(
            PropertyInfo SourceProperty)
        {
            return null;
        }
    }
}