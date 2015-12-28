// <copyright file="ODataNavigationMultipleResultsQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.OData.Query;

    using AMSLLC.Listener.Core;
    using AMSLLC.Listener.MetadataService;
    using AMSLLC.Listener.ODataService.Controllers.Base;
    using AMSLLC.Listener.ODataService.Services.FilterTransformer;
    using AMSLLC.Listener.ODataService.Services.Impl.FilterTransformer;
    using AMSLLC.Listener.Persistence.WNP;
    using AMSLLC.Listener.Repository.WNP;
    using Newtonsoft.Json;

    /// <summary>
    /// Query handler for multiple results navigation property query
    /// </summary>
    public class ODataNavigationMultipleResultsQueryHandler : IODataNavigationMultipleResultsQueryHandler
    {
        private readonly IMetadataProvider metadataProvider;
        private readonly IWNPUnitOfWork unitOfWork;

        private readonly IFilterTransformer filterTransformer;

        private Type parentEdmEntityClrType;
        private Type childEdmEntityClrType;
        private DbColumnList selectedFields;

        private int? skip;
        private int? top;

        private WhereClause sqlFilter;

        private List<KeyValuePair<string, object>> parentKey;

        private MetadataEntityModel parentModel;
        private MetadataEntityModel childModel;

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
            this.parentEdmEntityClrType = parentClrType;
            this.childEdmEntityClrType = childClrType;

            this.childModel = this.metadataProvider.GetModelMapping(this.childEdmEntityClrType);
            this.parentModel = this.metadataProvider.GetModelMapping(this.parentEdmEntityClrType);

            return this;
        }

        /// <summary>
        /// Sets the "raw", json-formatted keys of parent and child entity.
        /// </summary>
        /// <param name="rawParentKey">The json-formatted parent key</param>
        /// <returns>Current instance of the IODataNavigationMultipleResultsQueryHandler</returns>
        public IODataNavigationMultipleResultsQueryHandler WithKey(string rawParentKey)
        {
            if (this.childModel == null)
            {
                // this is a shortcut to avoid creating a separate builder hierarchy for
                // OnType call branch
                throw new InvalidOperationException("WithKey method should be called after OnTypes");
            }

            this.parentKey = Helper.GetKey(rawParentKey, this.childModel, true).ToList();

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
            this.sqlFilter.ConvertUtcParamsToLocalTime();

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
        public IEnumerable<object> Fetch()
        {
            // Generate SQL
            var sql = Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From($"{this.childModel.TableName}");

            // Add necessary joins
            Helper.PerformJoins(ref sql, this.childModel, this.relatedEntityModels);

            // Add key selector
            var whereClause =
                this.parentKey.Select(
                    (kvp, ind) => $"{this.childModel.TableName}.{this.childModel.ModelToColumnMappings[kvp.Key]}=@{ind}")
                    .Aggregate((s, s1) => $"{s} AND {s1}");

            object[] filterArgs = null;

            if (!string.IsNullOrWhiteSpace(this.sqlFilter?.Clause))
            {
                whereClause += $" AND {this.sqlFilter.Clause}";
                filterArgs = this.sqlFilter.PositionalParameters;
            }

            var whereArgs =
                this.parentKey.Select(kvp => kvp.Value).ToList();

            whereArgs.AddRange(filterArgs ?? new object[] { });

            sql = sql.Where(whereClause, whereArgs.ToArray());

            var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.Fetch<dynamic>(sql);
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