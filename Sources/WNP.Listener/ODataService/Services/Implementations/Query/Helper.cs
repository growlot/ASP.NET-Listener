// <copyright file="Helper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using MetadataService;
    using Newtonsoft.Json;
    using Persistence.Poco;
    using Utilities;

    internal static class Helper
    {
        public static Sql JoinWithParrent(Sql sql, MetadataEntityModel childModel, MetadataEntityModel parentModel)
        {
            var relationConfig =
                parentModel.EntityConfiguration.Relations.FirstOrDefault(information => information.TargetTableName == childModel.TableName);
            Debug.Assert(relationConfig != null, "relationConfig != null");

            sql = AddLeftJoin(sql, childModel.TableName, parentModel.TableName, relationConfig);

            return sql;
        }

        public static Sql PerformJoins(Sql sql, MetadataEntityModel mainModel, IEnumerable<MetadataEntityModel> relatedEntityModels)
        {
            foreach (var entityModel in relatedEntityModels)
            {
                var relationConfig =
                    mainModel.EntityConfiguration.Relations.FirstOrDefault(information => information.TargetTableName == entityModel.TableName);

                Debug.Assert(relationConfig != null, "relationConfig != null");

                sql = AddLeftJoin(sql, mainModel.TableName, entityModel.TableName, relationConfig);
            }

            return sql;
        }

        public static Dictionary<string, object> GetKey(string rawKey, MetadataEntityModel model, bool isParent)
        {
            var entityConfig = model.EntityConfiguration;

            var hasCompositeKey = entityConfig.Key?.Count() > 1;
            var jsonKey = rawKey;
            if (jsonKey == null)
            {
                throw new ArgumentException("Invalid key specified");
            }

            var keyDic = new Dictionary<string, object>();
            if (hasCompositeKey)
            {
                keyDic = jsonKey.ToCompositeKeyDictionary();
            }
            else
            {
                var keyColumn = isParent
                                    ? entityConfig.ParentKey.Where(s => s.ToUpperInvariant() != "OWNER").ToArray()[0]
                                          .ToUpperInvariant()
                                    : entityConfig.Key.ToArray()[0].ToUpperInvariant();

                var keyModelField = model.ColumnToModelMappings[keyColumn];
                var fieldInfo = model.FieldInfo.First(pair => pair.Key == keyModelField);

                keyDic.Add(keyModelField, DeserializeObject(jsonKey, fieldInfo));
            }

            return keyDic;
        }

        /// <summary>
        /// Adds the left join in the select statement.
        /// </summary>
        /// <param name="sql">The select SQL.</param>
        /// <param name="mainTableName">Name of the main select table.</param>
        /// <param name="joinTableName">Name of the table that must be joined.</param>
        /// <param name="relationConfig">The relation configuration, on how these tables must be joined.</param>
        /// <returns>The SQL statement with added left join clause.</returns>
        private static Sql AddLeftJoin(Sql sql, string mainTableName, string joinTableName, RelationInformation relationConfig)
        {
            var onClause =
                relationConfig.MatchOn.Select(
                    m => StringUtilities.Invariant($"{joinTableName}.{m.SourceColumn} = {mainTableName}.{m.TargetColumn}"))
                    .Aggregate((m1, m2) => StringUtilities.Invariant($"{m1} AND {m2}"));

            if (relationConfig.MatchValue != null)
            {
                onClause =
                    StringUtilities.Invariant($"{onClause} AND {relationConfig.MatchValue.TargetColumn} = '{relationConfig.MatchValue.TargetColumnValue}'");
            }

            sql = sql.LeftJoin(joinTableName).On(onClause);

            return sql;
        }

        private static object DeserializeObject(string jsonKey, KeyValuePair<string, MetadataFieldInfo> fieldInfo)
        {
            // DateTimeOffset value specified as the key, must be converted to database date format.
            if (fieldInfo.Value.ClrDataType == typeof(DateTimeOffset))
            {
                // it seems like this is the only adequate way to make JsonConvert correctly parse
                jsonKey = StringUtilities.Invariant($"'{jsonKey}'");
                var dateKey = (DateTimeOffset)JsonConvert.DeserializeObject(jsonKey, fieldInfo.Value.ClrDataType);
                return new DateTime(dateKey.ToLocalTime().Ticks);
            }

            return JsonConvert.DeserializeObject(jsonKey, fieldInfo.Value.ClrDataType);
        }
    }
}