﻿// <copyright file="ODataNavigationSingleResultQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Controllers.Base;
    using MetadataService;
    using Persistence.Poco;
    using Persistence.WNP;
    using Repository.WNP;
    using Services.Query;
    using Utilities;

    /// <summary>
    /// IODataNavigationSingleResultQueryHandler default implementation
    /// </summary>
    public class ODataNavigationSingleResultQueryHandler : IODataNavigationSingleResultQueryHandler
    {
        private readonly IMetadataProvider metadataProvider;

        private readonly IWNPUnitOfWork unitOfWork;

        private Type parentEdmEntityClrType;

        private Type childEdmEntityClrType;

        private List<KeyValuePair<string, object>> parentKey;
        private List<KeyValuePair<string, object>> childKey;

        private DbColumnList selectedFields;

        private MetadataEntityModel childModel;
        private MetadataEntityModel parentModel;

        private MetadataEntityModel[] relatedEntityModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataNavigationSingleResultQueryHandler"/> class.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider</param>
        /// <param name="unitOfWork">The unit of work</param>
        public ODataNavigationSingleResultQueryHandler(IMetadataProvider metadataProvider, IWNPUnitOfWork unitOfWork)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="parentClrType">Parent CLR type</param>
        /// <param name="childClrType">Child CLR type</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        public IODataNavigationSingleResultQueryHandler OnTypes(Type parentClrType, Type childClrType)
        {
            this.parentEdmEntityClrType = parentClrType;
            this.childEdmEntityClrType = childClrType;

            this.childModel = this.metadataProvider.GetModelMapping(this.childEdmEntityClrType);
            this.parentModel = this.metadataProvider.GetModelMapping(this.parentEdmEntityClrType);

            return this;
        }

        /// <summary>
        /// Sets the "raw", json-formatted key of entity to be retrieved.
        /// </summary>
        /// <param name="rawParentKey">The json-formatted parent key</param>
        /// <param name="rawChildKey">The json-formatted child key</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WithKeys", Justification = "It's a method name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OnTypes", Justification = "It's a method name.")]
        public IODataNavigationSingleResultQueryHandler WithKeys(string rawParentKey, string rawChildKey)
        {
            if (this.childModel == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // OnType call branch
                throw new InvalidOperationException("WithKeys method should be called after OnTypes");
            }

            this.parentKey = Helper.GetKey(rawParentKey, this.parentModel, false).ToList();
            this.childKey = Helper.GetKey(rawChildKey, this.childModel, false).ToList();

            return this;
        }

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SelectFields", Justification = "It's a method name.")]
        public IODataNavigationSingleResultQueryHandler SelectFields(string[] fields)
        {
            if (this.relatedEntityModels == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // Expand call branch
                throw new InvalidOperationException("SelectFields method should be called after Expand");
            }

            this.selectedFields = new DbColumnList(fields, this.childModel, this.relatedEntityModels);

            return this;
        }

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataNavigationSingleResultQueryHandler</returns>
        public IODataNavigationSingleResultQueryHandler Expand(string[] expands)
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
        public async Task<object> FetchAsync()
        {
            this.CheckMandatoryFields();

            // Generate SQL
            var sql = Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From(this.childModel.TableName);

            // Add join with parent
            sql = Helper.JoinWithParrent(sql, this.childModel, this.parentModel);

            // Add related entity joins
            sql = Helper.PerformJoins(sql, this.childModel, this.relatedEntityModels);

            // Add key selector
            var parentWhereClause =
                this.parentKey.Select(
                    (kvp, ind) => StringUtilities.Invariant($"{this.parentModel.TableName}.{this.parentModel.ModelToColumnMappings[kvp.Key]}=@{ind}"))
                    .Aggregate((s, s1) => StringUtilities.Invariant($"{s} AND {s1}"));

            var childWhereClause =
                this.childKey.Select(
                    (kvp, ind) => StringUtilities.Invariant($"{this.childModel.TableName}.{this.childModel.ModelToColumnMappings[kvp.Key]}=@{ind + this.parentKey.Count}"))
                    .Aggregate((s, s1) => StringUtilities.Invariant($"{s} AND {s1}"));

            var whereClause = StringUtilities.Invariant($"{parentWhereClause} AND {childWhereClause}");

            var whereArgs =
                this.parentKey.Select(kvp => kvp.Value).ToList();
            whereArgs.AddRange(
                this.childKey.Select(kvp => kvp.Value).ToList());

            sql = sql.Where(whereClause, whereArgs.ToArray());

            ////// Add key selector
            ////sql = sql.Where(
            ////        this.key.Select(
            ////            (kvp, ind) =>
            ////            StringUtilities.Invariant($"{this.childModel.TableName}.{this.childModel.ModelToColumnMappings[kvp.Key]}=@{ind}"))
            ////            .Aggregate((s, s1) => StringUtilities.Invariant($"{s} AND {s1}")),
            ////        this.key.Select(kvp => kvp.Value).ToArray());

            // Fetch the results in one big batch
            var dbContext = ((WNPUnitOfWork)this.unitOfWork).DbContext;

            var dbResults = await dbContext.FetchAsync<dynamic>(1, 1000, sql);
            var dbResultsDictionary = dbResults.Cast<IDictionary<string, object>>().ToArray();

            // Check obvious errors
            if (this.relatedEntityModels.Length == 0
                && this.childModel.EntityConfiguration.VirtualRelations.Count == 0
                && dbResultsDictionary.Length > 1)
            {
                throw new InvalidNumberOfRecordsException("Request returned more than 1 record.");
            }
            else if (dbResultsDictionary.Length == 0)
            {
                throw new EntityNotFoundException();
            }

            return
                new DynamicEntityConstructor(
                    this.metadataProvider,
                    this.childModel,
                    this.selectedFields,
                    this.relatedEntityModels,
                    this.childEdmEntityClrType,
                    dbResultsDictionary).GetEntity();
        }

        private void CheckMandatoryFields()
        {
            if (this.parentModel == null || this.childModel == null)
            {
                throw new InvalidOperationException("Resulting Type is not set");
            }

            if (this.parentKey == null || this.parentKey.Count == 0)
            {
                throw new InvalidOperationException("Required parent entity key is not set");
            }

            if (this.childKey == null || this.childKey.Count == 0)
            {
                throw new InvalidOperationException("Required child entity key is not set");
            }
        }
    }
}