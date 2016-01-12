// <copyright file="ModelKeyEqualityComparer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System.Collections.Generic;
    using System.Linq;
    using Controllers.Base;
    using MetadataService;

    internal class ModelKeyEqualityComparer : IEqualityComparer<IDictionary<string, object>>
    {
        private readonly MetadataEntityModel model;

        private readonly DbColumnList dbColumnList;

        private readonly IEnumerable<string> key;

        public ModelKeyEqualityComparer(MetadataEntityModel model, DbColumnList dbColumnList)
        {
            this.model = model;
            this.dbColumnList = dbColumnList;
            this.key = model.EntityConfiguration.Key;
        }

        public ModelKeyEqualityComparer(IEnumerable<string> key, MetadataEntityModel model, DbColumnList dbColumnList)
        {
            this.model = model;
            this.key = key;
            this.dbColumnList = dbColumnList;
        }

        public bool Equals(IDictionary<string, object> x, IDictionary<string, object> y)
        {
            return
                this.key.All(
                    keyName =>
                        {
                            var keyValueX = x[this.dbColumnList.GetDbQueryNameByModelColumnName(this.model.ColumnToModelMappings[keyName])];
                            var keyValueY = y[this.dbColumnList.GetDbQueryNameByModelColumnName(this.model.ColumnToModelMappings[keyName])];

                            if (keyValueX == null && keyValueY == null)
                            {
                                return true;
                            }

                            return keyValueX?.Equals(keyValueY) ?? false;
                        });
        }

        public int GetHashCode(IDictionary<string, object> obj)
        {
            if (obj == null)
            {
                return 0;
            }

            return
                this.key.Select(
                    keyName =>
                    obj[this.dbColumnList.GetDbQueryNameByModelColumnName(this.model.ColumnToModelMappings[keyName])]?
                        .GetHashCode() ?? 0).Aggregate((k, k1) => k + k1);
        }
    }
}