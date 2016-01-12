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
    using Persistence.WNP;
    using Utilities;

    internal static class Helper
    {
        public static void PerformJoins(ref Sql sql, MetadataEntityModel mainModel, IEnumerable<MetadataEntityModel> relatedEntityModels)
        {
            var modelConfig = mainModel.EntityConfiguration;
            var parentTable = modelConfig.TableName;

            foreach (var entityModel in relatedEntityModels)
            {
                var relationConfig =
                    modelConfig.Relations.FirstOrDefault(information => information.TargetTableName == entityModel.TableName);

                Debug.Assert(relationConfig != null, "relationConfig != null");

                var onClause =
                    relationConfig.MatchOn.Select(
                        m => StringUtilities.Invariant($"{entityModel.TableName}.{m.SourceColumn} = {parentTable}.{m.TargetColumn}"))
                        .Aggregate((m1, m2) => StringUtilities.Invariant($"{m1} AND {m2}"));

                if (relationConfig.MatchValue != null)
                {
                    onClause =
                        StringUtilities.Invariant($"{onClause} AND {relationConfig.MatchValue.TargetColumn} = '{relationConfig.MatchValue.TargetColumnValue}'");
                }

                sql = sql.LeftJoin(entityModel.TableName).On(onClause);
            }
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
                    ? entityConfig.ParentKey.ToArray()[0].ToUpperInvariant()
                    : entityConfig.Key.ToArray()[0].ToUpperInvariant();

                var keyModelField = model.ColumnToModelMappings[keyColumn];
                var fieldInfo = model.FieldInfo.First(pair => pair.Key == keyModelField);

                keyDic.Add(keyModelField, DeserializeObject(jsonKey, fieldInfo));
            }

            return keyDic;
        }

        private static object DeserializeObject(string jsonKey, KeyValuePair<string, MetadataFieldInfo> fieldInfo)
        {
            // it seems like this is the only adequate way to make JsonConvert correctly parse
            // DateTimeOffset type
            if (fieldInfo.Value.ClrDataType == typeof(DateTimeOffset))
            {
                jsonKey = StringUtilities.Invariant($"'{jsonKey}'");
            }

            return JsonConvert.DeserializeObject(jsonKey, fieldInfo.Value.ClrDataType);
        }
    }
}