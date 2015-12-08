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
    /// Helper class for generating list of columns to be SELECTed with random aliases.
    /// </summary>
    public class DbColumnList
    {
        private readonly MetadataEntityModel mainModel;

        private readonly string[] selectedFields;

        private readonly MetadataEntityModel[] relationModels;

        private readonly Random random = new Random(Guid.NewGuid().GetHashCode());

        private readonly List<string> queryColumnList = new List<string>();

        private readonly Dictionary<string, string> queryColToModelMapping = new Dictionary<string, string>();

        private readonly Dictionary<string, string> queryColToActualDbNameMapping = new Dictionary<string, string>();

        private readonly Dictionary<string, MetadataEntityModel> queryColToEntityModel = new Dictionary<string, MetadataEntityModel>();

        /// <summary>
        /// Initializes a new instance of the <see cref="DbColumnList"/> class.
        /// </summary>
        /// <param name="selectedFields">Array of selected fields from request.</param>
        /// <param name="mainModel">Main entity model.</param>
        /// <param name="relationModels">All relation models that are included in the request via $expand.</param>
        public DbColumnList(string[] selectedFields, MetadataEntityModel mainModel, params MetadataEntityModel[] relationModels)
        {
            this.selectedFields = selectedFields;
            this.mainModel = mainModel;
            this.relationModels = relationModels;

            this.Init();
        }

        /// <summary>
        /// Get random generated query column list for the specified model.
        /// </summary>
        /// <param name="model">MetadataEntityModel for which generate the list.</param>
        /// <returns>Random generated query column list for the specified model.</returns>
        public string[] GetQueryColumnListForModel(MetadataEntityModel model)
            => this.queryColToEntityModel.Where(kvp => kvp.Value == model).Select(kvp => kvp.Key).ToArray();

        /// <summary>
        /// Get the resulted query column list with aliases.
        /// </summary>
        /// <returns>The object array containing resulted query column list with aliases</returns>
        public object[] GetQueryColumnList()
            => this.queryColumnList.Cast<object>().ToArray();

        /// <summary>
        /// Get model column using generated query column name.
        /// </summary>
        /// <param name="dbQueryColumnName">Generated query column name to get model column name by.</param>
        /// <returns>Model column name.</returns>
        public string GetModelColumnByDbQueryName(string dbQueryColumnName)
            => this.queryColToModelMapping[dbQueryColumnName];

        /// <summary>
        /// Get MetadataEntityModel by generated query column name.
        /// </summary>
        /// <param name="dbQueryColumnName">Name of the database query column.</param>
        /// <returns>
        /// Entity Model related to the generated query column name.
        /// </returns>
        public MetadataEntityModel GetEntityModelByDbQueryName(string dbQueryColumnName)
            => this.queryColToEntityModel[dbQueryColumnName];

        public string GetDbQueryNameByModelColumnName(string modelColumnName)
            => this.queryColToModelMapping.FirstOrDefault(pair => pair.Value == modelColumnName).Key;

        public string GetActualDbNameByDbQueryName(string dbQueryName)
            => this.queryColToActualDbNameMapping.FirstOrDefault(pair => pair.Key == dbQueryName).Value;

        private void Init()
        {
            if (this.selectedFields != null)
            {
                foreach (var field in this.selectedFields)
                {
                    // this is parent entity
                    var dotIndex = field.IndexOf("/", StringComparison.Ordinal);
                    if (dotIndex == -1)
                    {
                        this.queryColumnList.Add(
                            $"{this.mainModel.TableName}.{this.mainModel.ModelToColumnMappings[field]} AS {this.GenerateRandomColumnName(this.mainModel, field, this.mainModel.ModelToColumnMappings[field])}");
                    }

                    // this is related entity
                    else
                    {
                        var modelName = field.Substring(0, dotIndex);
                        var model = this.relationModels?.First(entityModel => entityModel.ClassName == modelName);

                        if (model != null)
                        {
                            var fieldName = field.Substring(dotIndex + 1);
                            var dbColumnName = model.ModelToColumnMappings[fieldName];
                            this.queryColumnList.Add(
                                $"{model.TableName}.{dbColumnName} AS {this.GenerateRandomColumnName(model, fieldName, dbColumnName)}");
                        }
                    }
                }

                Action<MetadataEntityModel> addKeysActions = model =>
                    {
                        model.EntityConfiguration.Key.Map(
                            field =>
                            this.queryColumnList.Add(
                                $"{model.TableName}.{field} AS {this.GenerateRandomColumnName(model, model.ColumnToModelMappings[field], field)}"));
                    };

                addKeysActions(this.mainModel);
                this.relationModels?.Map(addKeysActions);

                foreach (var relation in this.mainModel.EntityConfiguration.VirtualRelations)
                {
                    relation.ColumnList.Select(s => s.ToUpperInvariant()).Map(
                        field =>
                        this.queryColumnList.Add(
                            $"{this.mainModel.TableName}.{field} AS {this.GenerateRandomColumnName(this.mainModel, this.mainModel.ColumnToModelMappings[field], field)}"));
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
                                    $"{model.TableName}.{field} AS {this.GenerateRandomColumnName(model, model.ColumnToModelMappings[field], field)}"));
                        };

                addColsActions(this.mainModel);
                this.relationModels?.Map(addColsActions);
            }
        }

        private string GenerateRandomColumnName(MetadataEntityModel entityModel, string modelColName, string actualDbName)
        {
            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var randomColumnName =
                new string(Enumerable.Repeat(Chars, 4).Select(s => s[this.random.Next(s.Length)]).ToArray());

            this.queryColToActualDbNameMapping.Add(randomColumnName, actualDbName);
            this.queryColToModelMapping.Add(randomColumnName, modelColName);
            this.queryColToEntityModel.Add(randomColumnName, entityModel);

            return randomColumnName;
        }
    }
}