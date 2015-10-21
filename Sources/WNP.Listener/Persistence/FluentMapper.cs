using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AMSLLC.Listener.Persistence
{
    public class FluentColumnMap
    {
        public ColumnInfo ColumnInfo { get; set; }
        public Func<object, object> FromDbConverter { get; set; }
        public Func<object, object> ToDbConverter { get; set; }

        public FluentColumnMap() { }
        public FluentColumnMap(ColumnInfo columnInfo) : this(columnInfo, null) { }
        public FluentColumnMap(ColumnInfo columnInfo, Func<object, object> fromDbConverter) : this(columnInfo, fromDbConverter, null) { }
        public FluentColumnMap(ColumnInfo columnInfo, Func<object, object> fromDbConverter, Func<object, object> toDbConverter)
        {
            this.ColumnInfo = columnInfo;
            this.FromDbConverter = fromDbConverter;
            this.ToDbConverter = toDbConverter;
        }
    }

    public abstract class FluentMapper<T> : IMapper
    {
        public Dictionary<string, FluentColumnMap> Mappings = new Dictionary<string, FluentColumnMap>();
        public TableInfo TableInfo = new TableInfo();

        protected FluentMapper(string tableName)
        {
            this.TableInfo.TableName = tableName;
        }

        protected FluentMapper(string tableName, string primaryKey)
        {
            this.TableInfo.TableName = tableName;
            this.TableInfo.PrimaryKey = primaryKey;
        }

        public TableInfo GetTableInfo(Type pocoType)
        {
            return this.TableInfo;
        }

        public ColumnInfo GetColumnInfo(PropertyInfo pocoProperty)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.Mappings.TryGetValue(pocoProperty.Name, out fluentMap))
                return fluentMap.ColumnInfo;
            return null;
        }

        public Func<object, object> GetFromDbConverter(PropertyInfo TargetProperty, Type SourceType)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.Mappings.TryGetValue(TargetProperty.Name, out fluentMap))
                return fluentMap.FromDbConverter;
            return null;
        }

        public Func<object, object> GetToDbConverter(PropertyInfo SourceProperty)
        {
            var fluentMap = default(FluentColumnMap);
            if (this.Mappings.TryGetValue(SourceProperty.Name, out fluentMap))
                return fluentMap.ToDbConverter;
            return null;
        }
    }

    public static class FluentMapperExtensions
    {
        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, bool primaryKey = false, bool readOnly = false) where T : class
        {
            return obj.Property(action, column, null, primaryKey, readOnly);
        }

        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, Func<object, object> fromDbConverter, bool primaryKey = false, bool readOnly = false) where T : class
        {
            return obj.Property(action, column, fromDbConverter, null, primaryKey, readOnly);
        }

        public static FluentMapper<T> Property<T, P>(this FluentMapper<T> obj, Expression<Func<T, P>> action, string column, Func<object, object> fromDbConverter, Func<object, object> toDbConverter, bool primaryKey = false, bool readOnly = false) where T : class
        {
            var expression = (MemberExpression)action.Body;
            string name = expression.Member.Name;

            obj.Mappings.Add(name, new FluentColumnMap(new ColumnInfo() { ColumnName = column, ResultColumn = readOnly }, fromDbConverter, toDbConverter));

            if (primaryKey)
            {
                if (!string.IsNullOrEmpty(obj.TableInfo.PrimaryKey))
                    obj.TableInfo.PrimaryKey += ",";
                obj.TableInfo.PrimaryKey += column;
            }

            return obj;
        }
    }
}