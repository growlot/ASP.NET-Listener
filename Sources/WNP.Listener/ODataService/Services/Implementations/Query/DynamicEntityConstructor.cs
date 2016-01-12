// <copyright file="DynamicEntityConstructor.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Controllers.Base;
    using MetadataService;
    using Utilities;

    internal class DynamicEntityConstructor
    {
        private readonly IMetadataProvider metadataProvider;

        private readonly MetadataEntityModel mainModel;

        private readonly DbColumnList selectedFields;

        private readonly Type childEdmEntityClrType;

        private readonly IDictionary<string, object>[] dbResults;

        private readonly MetadataEntityModel[] relatedEntityModels;

        public DynamicEntityConstructor(
            IMetadataProvider metadataProvider,
            MetadataEntityModel mainModel,
            DbColumnList selectedFields,
            MetadataEntityModel[] relatedEntityModels,
            Type childEdmEntityClrType,
            IDictionary<string, object>[] dbResults)
        {
            this.metadataProvider = metadataProvider;
            this.mainModel = mainModel;
            this.selectedFields = selectedFields;
            this.childEdmEntityClrType = childEdmEntityClrType;
            this.dbResults = dbResults;
            this.relatedEntityModels = relatedEntityModels ?? new MetadataEntityModel[0];
        }

        public object GetEntity()
        {
            var result = this.CreateEdmEntity();

            // what if key not selected by user?

            // Fill
            this.FillResult(this.mainModel, result);

            // Fill internal relations
            this.FillInternalRelations(result);

            // Fill all expanded fields
            this.relatedEntityModels.Map(model => this.FillResult(model, result));

            return result;
        }

        private void FillInternalRelations(object instance)
        {
            var model = this.mainModel;

            var internalRelations = model.EntityConfiguration.VirtualRelations;
            foreach (var relation in internalRelations)
            {
                var resultedRecords =
                    this.dbResults.Distinct(
                        new ModelKeyEqualityComparer(relation.Discriminator, model, this.selectedFields))
                        .ToArray();

                var virtualType =
                    this.metadataProvider.GetEntityType(
                        StringUtilities.Invariant($"{this.metadataProvider.ODataModelNamespace}.{relation.VirtualEntityName}"));

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

        private void FillResult(MetadataEntityModel model, object instance)
        {
            var resultedRecords =
                this.dbResults.Distinct(
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

                        var property = this.childEdmEntityClrType.GetProperty(fieldName);
                        property.SetValue(instance, Converters.Convert(colValue, property.PropertyType));
                    }
                }
            }
            else
            {
                var clrType =
                    this.metadataProvider.GetEntityType(
                        StringUtilities.Invariant($"{this.metadataProvider.ODataModelNamespace}.{model.ClassName}"));

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

        private object CreateEdmEntity()
        {
            // Create default instance of the target CLR type of entity
            var entityInstance = Activator.CreateInstance(this.childEdmEntityClrType);

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

        private PropertyInfo GetRelationPropertyName(string className)
        {
            // Enumerate all properties for further use
            var edmTypeProperties = this.childEdmEntityClrType.GetProperties();

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

        private void SetDefaultPropertyValue(object entityInstance, string className)
        {
            // Get the property of entity that corresponds to the requested related entity model
            var property = this.GetRelationPropertyName(className);

            Debug.Assert(property != null, "property != null");

            // Set default value for that property
            var propInstance = Activator.CreateInstance(property.PropertyType);
            property.SetValue(entityInstance, propInstance);
        }
    }
}