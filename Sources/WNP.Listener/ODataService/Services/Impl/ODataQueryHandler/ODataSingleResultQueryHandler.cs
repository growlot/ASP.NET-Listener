// <copyright file="ODataSingleResultQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;

    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.MetadataService;
    using AMSLLC.Listener.ODataService.Controllers.Base;
    using AMSLLC.Listener.Persistence.WNP;
    using AMSLLC.Listener.Utilities;

    using Microsoft.OData.Core.UriParser.Semantic;
    using Microsoft.OData.Edm;

    using Newtonsoft.Json;

    /// <summary>
    /// IODataSingleResultQueryHandler default implementation
    /// </summary>
    public class ODataSingleResultQueryHandler : IODataSingleResultQueryHandler
    {
        private readonly IMetadataProvider metadataProvider;

        private readonly IUnitOfWork unitOfWork;

        private Type edmEntityClrType;

        private KeyValuePair<string, object>[] key;

        private DbColumnList selectedFields;

        private MetadataEntityModel mainModel;

        private MetadataEntityModel[] relatedEntityModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataSingleResultQueryHandler"/> class.
        /// </summary>
        /// <param name="metadataProvider">The MetadataProvider</param>
        /// <param name="unitOfWork">Current UnitOfWork</param>
        public ODataSingleResultQueryHandler(IMetadataProvider metadataProvider, IUnitOfWork unitOfWork)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="clrType">The CLR type</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        public IODataSingleResultQueryHandler OnType(Type clrType)
        {
            this.edmEntityClrType = clrType;
            this.mainModel = this.metadataProvider.GetModelMapping(this.edmEntityClrType);

            return this;
        }

        /// <summary>
        /// Sets the "raw", json-formatted key of entity to be retrieved.
        /// </summary>
        /// <param name="rawKey">The json-formatted key</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        public IODataSingleResultQueryHandler WithKey(string rawKey)
        {
            if (this.mainModel == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // OnType call branch
                throw new InvalidOperationException("WithKey method should be called after OnType");
            }

            var entityConfig = this.mainModel.EntityConfiguration;

            var hasCompositeKey = entityConfig.Key?.Count() > 1;
            var jsonKey = rawKey;
            if (jsonKey == null)
            {
                throw new ArgumentException("Invalid key specified");
            }

            var key = new Dictionary<string, object>();
            if (hasCompositeKey)
            {
                key = jsonKey.ToCompositeKeyDictionary();
            }
            else
            {
                key.Add(
                    this.mainModel.ColumnToModelMappings[entityConfig.Key.ToArray()[0].ToUpperInvariant()],
                    JsonConvert.DeserializeObject(jsonKey));
            }

            this.key = key.ToArray();

            return this;
        }

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        public IODataSingleResultQueryHandler SelectFields(string[] fields)
        {
            if (this.relatedEntityModels == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // Expand call branch
                throw new InvalidOperationException("SelectFields method should be called after Expand");
            }

            this.selectedFields = new DbColumnList(fields, this.mainModel, this.relatedEntityModels);

            return this;
        }

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataSingleResultQueryHandler</returns>
        public IODataSingleResultQueryHandler Expand(string[] expands)
        {
            this.relatedEntityModels =
                expands?.Select(clrEntityType => this.metadataProvider.GetModelMapping(clrEntityType))
                    .Where(model => model != null)
                    .ToArray() ?? new MetadataEntityModel[0];

            return this;
        }

        /// <summary>
        /// Fetches the fully formatted resulting instance of the requested entity.
        /// </summary>
        /// <returns>Instance of the entity class, defined in OData model assembly.</returns>
        public object FetchSingle()
        {
            this.CheckMandatoryFields();

            // Generate SQL
            var sql =
                Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From($"{this.mainModel.TableName}");

            // Add necessary joins
            sql = this.GenerateJoins(sql);

            // Add key selector
            sql = sql.Where(
                    this.key.Select(
                        (kvp, ind) =>
                        $"{this.mainModel.TableName}.{this.mainModel.ModelToColumnMappings[kvp.Key]}=@{ind}")
                        .Aggregate((s, s1) => $"{s} AND {s1}"),
                    this.key.Select(kvp => kvp.Value).ToArray());

            // Fetch the results in one big batch
            var dbContext = ((WNPUnitOfWork)this.unitOfWork).DbContext;

            var dbResults = dbContext.Fetch<dynamic>(sql).Cast<IDictionary<string, object>>().ToArray();

            // Check obvious errors
            if (this.relatedEntityModels.Length == 0
                && this.mainModel.EntityConfiguration.VirtualRelations.Count == 0
                && dbResults.Length > 1)
            {
                throw new InvalidNumberOfRecordsException("Request returned more than 1 record.");
            }
            else if (dbResults.Length == 0)
            {
                throw new EntityNotFoundException();
            }

            var result = this.CreateEdmEntity();

            // what if key not selected by user?

            // Fill
            this.FillResult(this.mainModel, dbResults, result);

            // Fill internal relations
            this.FillInternalRelations(dbResults, result);

            // Fill all expanded fields
            this.relatedEntityModels.Map(model => this.FillResult(model, dbResults, result));

            return result;
        }

        private void FillInternalRelations(IEnumerable<IDictionary<string, object>> dbResults, object instance)
        {
            var model = this.mainModel;

            var internalRelations = model.EntityConfiguration.VirtualRelations;
            foreach (var relation in internalRelations)
            {
                var resultedRecords =
                    dbResults.Distinct(
                        new ModelKeyEqualityComparer(relation.Discriminator, model, this.selectedFields))
                        .ToArray();

                var virtualType =
                    this.metadataProvider.GetEntityType(
                        $"{this.metadataProvider.ODataModelNamespace}.{relation.VirtualEntityName}");

                var expandProperty = this.GetRelationPropertyName(relation.VirtualEntityName);
                var isCollection = expandProperty.PropertyType.IsGenericType
                                   && expandProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);

                foreach (var record in resultedRecords)
                {
                    var relationInstance =
                        Activator.CreateInstance(
                            virtualType);

                    foreach (var dbColName in relation.ColumnList)
                    {
                        var fieldName = model.ColumnToModelMappings[dbColName.ToUpperInvariant()];
                        var dbQueryName =
                            this.selectedFields.GetDbQueryNameByModelColumnName(fieldName);

                        // this field wasn't selected
                        if (dbQueryName == null)
                        {
                            continue;
                        }

                        var colValue = record[dbQueryName];

                        var property = virtualType.GetProperty(fieldName);
                        property.SetValue(relationInstance, Converters.Convert(colValue, property.PropertyType));
                    }

                    if (isCollection)
                    {
                        var expandPropertyInstance = expandProperty.GetMethod.Invoke(instance, new object[] { });
                        var addMethod = expandPropertyInstance.GetType().GetMethod("Add");
                        addMethod.Invoke(expandPropertyInstance, new object[] { relationInstance });
                    }
                    else
                    {
                        expandProperty.SetValue(instance, relationInstance);
                    }
                }
            }
        }

        private void FillResult(MetadataEntityModel model, IEnumerable<IDictionary<string, object>> dbResults, object instance)
        {
            var resultedRecords =
                dbResults.Distinct(
                    new ModelKeyEqualityComparer(model, this.selectedFields)).ToArray();

            var dbColumnList = this.selectedFields.GetQueryColumnListForModel(model);

            if (model == this.mainModel)
            {
                foreach (var record in resultedRecords)
                {
                    foreach (var dbColName in dbColumnList)
                    {
                        var colValue = record[dbColName];
                        var fieldName = this.selectedFields.GetModelColumnByDbQueryName(dbColName);

                        var dbName = this.selectedFields.GetActualDbNameByDbQueryName(dbColName);
                        if (this.mainModel.EntityConfiguration.IsFieldInVirtualRelation(dbName))
                        {
                            continue;
                        }

                        var property = this.edmEntityClrType.GetProperty(fieldName);
                        property.SetValue(instance, Converters.Convert(colValue, property.PropertyType));
                    }
                }
            }
            else
            {
                var clrType =
                    this.metadataProvider.GetEntityType(
                        $"{this.metadataProvider.ODataModelNamespace}.{model.ClassName}");

                var expandProperty = this.GetRelationPropertyName(model.ClassName);
                var isCollection = expandProperty.PropertyType.IsGenericType
                                   && expandProperty.PropertyType.GetGenericTypeDefinition() == typeof(List<>);

                foreach (var record in resultedRecords)
                {
                    var relationInstance = Activator.CreateInstance(clrType);

                    // if this is empty instance, just continue
                    if (dbColumnList.All(col => record[col] == null))
                    {
                        continue;
                    }

                    foreach (var dbColName in dbColumnList)
                    {
                        var colValue = record[dbColName];
                        var fieldName = this.selectedFields.GetModelColumnByDbQueryName(dbColName);

                        var property = clrType.GetProperty(fieldName);
                        property.SetValue(relationInstance, Converters.Convert(colValue, property.PropertyType));
                    }

                    if (isCollection)
                    {
                        var expandPropertyInstance = expandProperty.GetMethod.Invoke(instance, new object[] { });
                        var addMethod = expandPropertyInstance.GetType().GetMethod("Add");
                        addMethod.Invoke(expandPropertyInstance, new object[] { relationInstance });
                    }
                    else
                    {
                        expandProperty.SetValue(instance, relationInstance);
                    }
                }
            }
        }

        private PropertyInfo GetRelationPropertyName(string className)
        {
            // Enumerate all properties for further use
            var edmTypeProperties = this.edmEntityClrType.GetProperties();

            // Enumerate all collection properties for further use
            var collectionProperties =
                edmTypeProperties
                    .Where(
                        info =>
                        info.PropertyType.IsGenericType
                        && info.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                    .ToArray();

            return
                collectionProperties.FirstOrDefault(
                    info => info.PropertyType.GetGenericArguments()[0].Name == className)
                ?? edmTypeProperties.FirstOrDefault(info => info.PropertyType.Name == className);
        }

        private object CreateEdmEntity()
        {
            // Create default instance of the target CLR type of entity
            var entityInstance = Activator.CreateInstance(this.edmEntityClrType);

            foreach (var entityModel in this.relatedEntityModels)
            {
                this.SetDefaultPropertyValue(entityInstance, entityModel.ClassName);
            }

            foreach (var relation in this.mainModel.EntityConfiguration.VirtualRelations)
            {
                this.SetDefaultPropertyValue(entityInstance, relation.VirtualEntityName);
            }

            return entityInstance;
        }

        private void SetDefaultPropertyValue(object entityInstance, string className)
        {
            // Get the property of entity that corresponds to the requested related entity model
            var property = this.GetRelationPropertyName(className);

            Debug.Assert(property != null, "property != null");

            // Set default value for that property
            var propInstance = Activator.CreateInstance(property.PropertyType);
            property.SetValue(entityInstance, propInstance);
        }

        private void CheckMandatoryFields()
        {
            if (this.edmEntityClrType == null || this.mainModel == null)
            {
                throw new InvalidOperationException("Resulting Type is not set");
            }

            if (this.key == null || this.key.Length == 0)
            {
                throw new InvalidOperationException("Required entity key is not set");
            }
        }

        private Sql GenerateJoins(Sql sql)
        {
            var modelConfig = this.mainModel.EntityConfiguration;
            var parentTable = modelConfig.TableName;

            foreach (var entityModel in this.relatedEntityModels)
            {
                var relationConfig =
                    modelConfig.Relations.FirstOrDefault(information => information.TargetTableName == entityModel.TableName);

                Debug.Assert(relationConfig != null, "relationConfig != null");

                var onClause =
                    relationConfig.MatchOn.Select(
                        m => $"{entityModel.TableName}.{m.SourceColumn} = {parentTable}.{m.TargetColumn}")
                        .Aggregate((m1, m2) => $"{m1} AND {m2}");

                if (relationConfig.MatchValue != null)
                {
                    onClause =
                        $"{onClause} AND {relationConfig.MatchValue.TargetColumn} = '{relationConfig.MatchValue.TargetColumnValue}'";
                }

                sql = sql.LeftJoin(entityModel.TableName).On(onClause);
            }

            return sql;
        }
    }
}