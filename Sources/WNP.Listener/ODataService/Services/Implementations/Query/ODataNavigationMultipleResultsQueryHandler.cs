﻿// <copyright file="ODataNavigationMultipleResultsQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.OData.Query;
    using Controllers.Base;
    using MetadataService;
    using Persistence.Poco;
    using Persistence.WNP;
    using Repository.WNP;
    using Services.Filter;
    using Services.Query;
    using Utilities;

    /// <summary>
    /// Query handler for multiple results navigation property query
    /// </summary>
    public class ODataNavigationMultipleResultsQueryHandler : IODataNavigationMultipleResultsQueryHandler
    {
        private readonly IMetadataProvider metadataProvider;
        private readonly IWNPUnitOfWork unitOfWork;

        private readonly IFilterTransformer filterTransformer;

        private DbColumnList selectedFields;

        private int? skip;
        private int? top;

        private WhereClause sqlFilter;

        private List<KeyValuePair<string, object>> parentKey;

        private Type childEdmEntityClrType;
        private Type parentEdmEntityClrType;

        private MetadataEntityModel childModel;
        private MetadataEntityModel parentModel;

        private MetadataEntityModel[] relatedEntityModels;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataNavigationMultipleResultsQueryHandler"/> class.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider</param>
        /// <param name="unitOfWork">The unit of work</param>
        /// <param name="filterTransformer">The filter transformer</param>
        public ODataNavigationMultipleResultsQueryHandler(IMetadataProvider metadataProvider, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
            this.filterTransformer = filterTransformer;
        }

        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="parentClrType">Parent CLR type</param>
        /// <param name="childClrType">Child CLR type</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler OnTypes(Type parentClrType, Type childClrType)
        {
            this.childEdmEntityClrType = childClrType;
            this.parentEdmEntityClrType = parentClrType;

            this.childModel = this.metadataProvider.GetModelMapping(this.childEdmEntityClrType);
            this.parentModel = this.metadataProvider.GetModelMapping(this.parentEdmEntityClrType);

            return this;
        }

        /// <summary>
        /// Sets the "raw", json-formatted keys of parent and child entity.
        /// </summary>
        /// <param name="rawParentKey">The json-formatted parent key</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "WithKey", Justification = "It's a method name.")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "OnTypes", Justification = "It's a method name.")]
        public IODataNavigationMultipleResultsQueryHandler WithKey(string rawParentKey)
        {
            if (this.childModel == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // OnType call branch
                throw new InvalidOperationException("WithKey method should be called after OnTypes");
            }

            this.parentKey = Helper.GetKey(rawParentKey, this.parentModel, false).ToList();

            return this;
        }

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler SelectFields(string[] fields)
        {
            this.selectedFields = new DbColumnList(fields, this.childModel, this.relatedEntityModels);

            return this;
        }

        /// <summary>
        /// Defines OData filter to be applied to the current query.
        /// TODO: currently, seems to be the only adequate way to put this info is via FilterQueryOption.
        /// </summary>
        /// <param name="filterQueryOption">The OData filter query option</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler Filter(FilterQueryOption filterQueryOption)
        {
            this.sqlFilter = this.filterTransformer.TransformFilterQueryOption(filterQueryOption, this.parentKey.Count);

            // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
            this.sqlFilter.ConvertUtcParametersToLocalTime();

            return this;
        }

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler Expand(string[] expands)
        {
            this.relatedEntityModels =
                expands?.Select(clrEntityType => this.metadataProvider.GetModelMapping(clrEntityType))
                    .Where(model => model != null)
                    .ToArray() ?? new MetadataEntityModel[0];

            return this;
        }

        /// <summary>
        /// Defines how much records should be skipped.
        /// </summary>
        /// <param name="count">Skip count</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler Skip(int? count)
        {
            this.skip = count;

            return this;
        }

        /// <summary>
        /// Defines how much records should be taken.
        /// </summary>
        /// <param name="count">Top count</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler Top(int? count)
        {
            this.top = count;

            return this;
        }

        /// <summary>
        /// Fetches the fully formatted collection of resulting instances of the requested entity.
        /// </summary>
        /// <returns>Instance of the entity class, defined in OData model assembly.</returns>
        public async Task<IEnumerable<object>> FetchAsync()
        {
            // Generate SQL
            var sql = Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From(this.childModel.TableName);

            // Add join with parent
            sql = Helper.JoinWithParrent(sql, this.childModel, this.parentModel);

            // Add related entity joins
            sql = Helper.PerformJoins(sql, this.childModel, this.relatedEntityModels);

            // Add key selector
            var whereClause =
                this.parentKey.Select(
                    (kvp, ind) => StringUtilities.Invariant($"{this.parentModel.TableName}.{this.parentModel.ModelToColumnMappings[kvp.Key]}=@{ind}"))
                    .Aggregate((s, s1) => StringUtilities.Invariant($"{s} AND {s1}"));

            object[] filterArgs = null;

            if (!string.IsNullOrWhiteSpace(this.sqlFilter?.Clause))
            {
                whereClause += StringUtilities.Invariant($" AND {this.sqlFilter.Clause}");
                filterArgs = this.sqlFilter.PositionalParameters;
            }

            var whereArgs =
                this.parentKey.Select(kvp => kvp.Value).ToList();

            whereArgs.AddRange(filterArgs ?? new object[] { });

            sql = sql.Where(whereClause, whereArgs.ToArray());

            var dbResults = await ((WNPUnitOfWork)this.unitOfWork).DbContext.FetchAsync<dynamic>(1, 1000, sql);
            var results = dbResults.Cast<IDictionary<string, object>>().ToArray();

            var fullKey = this.childModel.EntityConfiguration.FullKey;

            var dbQueryKey =
                fullKey.Select(
                    s =>
                    this.selectedFields.GetDbQueryNameByModelColumnName(this.childModel.ColumnToModelMappings[s])
                        .ToUpperInvariant());

            // grouping by the full entity key for the virtual relations case
            var recordGroups =
                results.GroupBy(
                    row =>
                    new DynamicTuple<object>(dbQueryKey.Select(column => row[column])));

            if (this.skip != null)
            {
                recordGroups = recordGroups.Skip(this.skip.Value);
            }

            if (this.top != null)
            {
                recordGroups = recordGroups.Take(this.top.Value);
            }

            var resultList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(this.childEdmEntityClrType));

            foreach (var recordGroup in recordGroups)
            {
                var entityInstance =
                    new DynamicEntityConstructor(
                        this.metadataProvider,
                        this.childModel,
                        this.selectedFields,
                        this.relatedEntityModels,
                        this.childEdmEntityClrType,
                        recordGroup.ToArray()).GetEntity();

                resultList.Add(entityInstance);
            }

            return (IEnumerable<object>)resultList;
        }
    }
}