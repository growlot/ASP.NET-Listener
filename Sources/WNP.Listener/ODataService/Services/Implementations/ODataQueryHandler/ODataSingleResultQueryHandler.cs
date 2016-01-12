// <copyright file="ODataSingleResultQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.ODataQueryHandler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Controllers.Base;
    using Domain;
    using MetadataService;
    using Newtonsoft.Json;
    using Persistence.WNP;

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
        public object Fetch()
        {
            this.CheckMandatoryFields();

            // Generate SQL
            var sql =
                Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From($"{this.mainModel.TableName}");

            // Add necessary joins
            Helper.PerformJoins(ref sql, this.mainModel, this.relatedEntityModels);

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

            if (dbResults.Length == 0)
            {
                throw new EntityNotFoundException();
            }

            return
               new DynamicEntityConstructor(
                   this.metadataProvider,
                   this.mainModel,
                   this.selectedFields,
                   this.relatedEntityModels,
                   this.edmEntityClrType,
                   dbResults).GetEntity();
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
    }
}