// <copyright file="DBColumnList.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.OData.Query;

    using AMSLLC.Listener.MetadataService;

    /// <summary>
    /// </summary>
    public class DbColumnList
    {
        private readonly MetadataEntityModel mainModel;

        private readonly SelectExpandQueryOption queryOption;

        private readonly MetadataEntityModel[] relationModels;

        private readonly Random random = new Random();

        private readonly List<string> queryColumnList = new List<string>();

        private readonly Dictionary<string, string> randomQueryColToModelMapping = new Dictionary<string, string>();

        private Dictionary<string, MetadataEntityModel> randomQueryColToEntityModel = new Dictionary<string, MetadataEntityModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumnList"/> class.
        /// </summary>
        /// <param name="queryOption"></param>
        /// <param name="mainModel"></param>
        /// <param name="relationModels"></param>
        public DbColumnList(SelectExpandQueryOption queryOption, MetadataEntityModel mainModel, params MetadataEntityModel[] relationModels)
        {
            this.queryOption = queryOption;
            this.mainModel = mainModel;
            this.relationModels = relationModels;

            var fieldList = queryOption?.RawSelect?.Split(',');

            if (fieldList != null)
            {
                foreach (var field in fieldList)
                {
                    // this is parent entity
                    var dotIndex = field.IndexOf(".");
                    if (dotIndex == -1)
                    {
                        this.queryColumnList.Add(
                            $"{mainModel.TableName}.{mainModel.ModelToColumnMappings[field]} AS {this.GenerateRandomColumnName(mainModel, field)}");
                    }

                    // this is related entity
                    else
                    {
                        var modelName = field.Substring(0, dotIndex);
                        var model = relationModels?.First(entityModel => entityModel.ClassName == modelName);

                        if (model != null)
                        {
                            var dbColumnName = model.ModelToColumnMappings[field.Substring(dotIndex)];
                            this.queryColumnList.Add(
                                $"{model.TableName}.{dbColumnName} AS {this.GenerateRandomColumnName(model, field)}");
                        }
                    }
                }
            }
            else
            {
                Action<MetadataEntityModel> addColsActions =
                    model =>
                        {
                            model.ModelToColumnMappings.Values.Map(
                                field =>
                                this.queryColumnList.Add(
                                    $"{model.TableName}.{field} AS {this.GenerateRandomColumnName(model, model.ColumnToModelMappings[field])}"));
                        };

                addColsActions(mainModel);
                relationModels?.Map(addColsActions);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object[] GetQueryColumnList()
            => this.queryColumnList.Cast<object>().ToArray();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbQueryColumnName"></param>
        /// <returns></returns>
        public string GetModelColumnByDbQueryName(string dbQueryColumnName)
            => this.randomQueryColToModelMapping[dbQueryColumnName];

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public MetadataEntityModel GetEntityModelByDbQueryName(string dbQueryColumnName)
            => this.randomQueryColToEntityModel[dbQueryColumnName];

        private string GenerateRandomColumnName(MetadataEntityModel entityModel, string modelColName)
        {
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var randomColumnName =
                new string(Enumerable.Repeat(Chars, 4).Select(s => s[this.random.Next(s.Length)]).ToArray());

            this.randomQueryColToModelMapping.Add(randomColumnName, modelColName);
            this.randomQueryColToEntityModel.Add(randomColumnName, entityModel);

            return randomColumnName;
        }
    }
}