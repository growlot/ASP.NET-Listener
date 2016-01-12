// <copyright file="ODataMultipleResultsQueryHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Implementations.Query
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.OData.Query;
    using Controllers.Base;
    using Core;
    using MetadataService;
    using Persistence.WNP;
    using Repository.WNP;
    using Services.Filter;
    using Services.Query;

    /// <summary>
    /// Query handler for multiple results query
    /// </summary>
    public class ODataMultipleResultsQueryHandler : IODataMultipleResultsQueryHandler
    {
        private readonly IMetadataProvider metadataProvider;
        private readonly IWNPUnitOfWork unitOfWork;

        private readonly IFilterTransformer filterTransformer;

        private Type edmEntityClrType;

        private MetadataEntityModel mainModel;

        private MetadataEntityModel[] relatedEntityModels;

        private DbColumnList selectedFields;

        private WhereClause sqlFilter;

        private int? skip;

        private int? top;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataMultipleResultsQueryHandler"/> class.
        /// </summary>
        /// <param name="metadataProvider">The metadata provider</param>
        /// <param name="unitOfWork">The unit of work</param>
        /// <param name="filterTransformer">The filter transformer</param>
        public ODataMultipleResultsQueryHandler(IMetadataProvider metadataProvider, IWNPUnitOfWork unitOfWork, IFilterTransformer filterTransformer)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
            this.filterTransformer = filterTransformer;
        }

        /// <summary>
        /// Defines on what type this query will be built upon.
        /// </summary>
        /// <param name="clrType">Parent CLR type</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        public IODataMultipleResultsQueryHandler OnType(Type clrType)
        {
            this.edmEntityClrType = clrType;
            this.mainModel = this.metadataProvider.GetModelMapping(this.edmEntityClrType);

            return this;
        }

        /// <summary>
        /// Defines what fields should be selected.
        /// Currently, only main entity fields are supported due to the limitation of OData implementation in WebAPI.
        /// </summary>
        /// <param name="fields">The list of requested fields</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SelectFields", Justification = "It's a method name.")]
        public IODataMultipleResultsQueryHandler SelectFields(string[] fields)
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
        /// Defines OData filter to be applied to the current query.
        /// TODO: currently, seems to be the only adequate way to put this info is via FilterQueryOption.
        /// </summary>
        /// <param name="filterQueryOption">The OData filter query option</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        public IODataMultipleResultsQueryHandler Filter(FilterQueryOption filterQueryOption)
        {
            if (filterQueryOption != null)
            {
                this.sqlFilter = this.filterTransformer.TransformFilterQueryOption(filterQueryOption);

                // convert parameters supplied as UTC time to local time, because WNP saves values as local time in db
                this.sqlFilter.ConvertUtcParametersToLocalTime();
            }

            return this;
        }

        /// <summary>
        /// Defines what fields should be expanded on the entity.
        /// </summary>
        /// <param name="expands">The list of clr type names of the fields to be expanded.</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        public IODataMultipleResultsQueryHandler Expand(string[] expands)
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
        public IODataMultipleResultsQueryHandler Skip(int? count)
        {
            this.skip = count;

            return this;
        }

        /// <summary>
        /// Defines how much records should be taken.
        /// </summary>
        /// <param name="count">Top count</param>
        /// <returns>Current instance of the IODataMultipleResultsQueryHandler</returns>
        public IODataMultipleResultsQueryHandler Top(int? count)
        {
            this.top = count;

            return this;
        }

        /// <summary>
        /// Fetches the fully formatted collection of resulting instances of the requested entity.
        /// </summary>
        /// <returns>Set of the entity class instances, defined in OData model assembly.</returns>
        public IEnumerable<object> Fetch()
        {
            var sql = Sql.Builder.Select(this.selectedFields.GetQueryColumnList()).From(this.mainModel.TableName);

            // Add necessary joins
            Helper.PerformJoins(ref sql, this.mainModel, this.relatedEntityModels);

            if (!string.IsNullOrWhiteSpace(this.sqlFilter?.Clause))
            {
                sql = sql.Where(this.sqlFilter.Clause, this.sqlFilter.PositionalParameters);
            }

            var dbResults = ((WNPUnitOfWork)this.unitOfWork).DbContext.Fetch<dynamic>(sql);
            var results = dbResults.Cast<IDictionary<string, object>>().ToArray();

            // create actual result object we will be sending over the wire
            var resultList = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(this.edmEntityClrType));

            // grouping by the full entity key for the virtual relations case
            var fullKey = this.mainModel.EntityConfiguration.Key;
            var dbQueryKey =
                fullKey.Select(
                    s =>
                    this.selectedFields.GetDbQueryNameByModelColumnName(this.mainModel.ColumnToModelMappings[s])
                        .ToUpperInvariant());

            var recordGroups =
                results.GroupBy(row => new DynamicTuple<object>(dbQueryKey.Select(column => row[column])));

            if (this.skip != null)
            {
                recordGroups = recordGroups.Skip(this.skip.Value);
            }

            if (this.top != null)
            {
                recordGroups = recordGroups.Take(this.top.Value);
            }

            foreach (var recordGroup in recordGroups)
            {
                var entityInstance =
                    new DynamicEntityConstructor(
                        this.metadataProvider,
                        this.mainModel,
                        this.selectedFields,
                        this.relatedEntityModels,
                        this.edmEntityClrType,
                        recordGroup.ToArray()).GetEntity();

                resultList.Add(entityInstance);
            }

            return (IEnumerable<object>)resultList;
        }
    }
}