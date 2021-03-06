﻿// <copyright file="UnboundActionsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using ApplicationService;
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Repository.WNP;
    using Services.Filter;

    /// <summary>
    /// Controller for Unbound OData Actions
    /// </summary>
    [ActionPrefix("Unbound")]
    public class UnboundActionsController : WNPController, IUnboundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnboundActionsController" /> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        /// <param name="commandBus">The command bus.</param>
        public UnboundActionsController(
            IMetadataProvider metadataService,
            IWNPUnitOfWork unitOfWork,
            IFilterTransformer filterTransformer,
            IActionConfigurator actionConfigurator,
            ICommandBus commandBus)
            : base(metadataService, unitOfWork, filterTransformer, actionConfigurator, commandBus)
        {
        }

        /// <summary>
        /// Test for unbound Action.
        /// </summary>
        /// <param name="testValue">The test string.</param>
        /// <returns>Same string.</returns>
        [UnboundAction]
        public static string Test(string testValue)
        {
            return testValue;
        }
    }
}