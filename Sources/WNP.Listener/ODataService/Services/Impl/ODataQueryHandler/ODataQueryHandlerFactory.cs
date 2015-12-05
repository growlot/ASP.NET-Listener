// <copyright file="ODataQueryHandlerFactory.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using AMSLLC.Listener.Domain;
    using AMSLLC.Listener.MetadataService;
    using AMSLLC.Listener.Repository.WNP;

    /// <summary>
    /// Factory class for creating fluent OData request handlers
    /// </summary>
    public class ODataQueryHandlerFactory : IODataQueryHandlerFactory
    {
        private readonly IMetadataProvider metadataProvider;

        private readonly IWNPUnitOfWork unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODataQueryHandlerFactory"/> class.
        /// </summary>
        /// <param name="metadataProvider">The MetadataProvider.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public ODataQueryHandlerFactory(IMetadataProvider metadataProvider, IWNPUnitOfWork unitOfWork)
        {
            this.metadataProvider = metadataProvider;
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates new query handler that will return single result.
        /// </summary>
        /// <returns>IODataSingleResultQueryHandler implementation instance</returns>
        public IODataSingleResultQueryHandler NewSingleResultQuery()
        {
            return new ODataSingleResultQueryHandler(this.metadataProvider, this.unitOfWork);
        }
    }
}