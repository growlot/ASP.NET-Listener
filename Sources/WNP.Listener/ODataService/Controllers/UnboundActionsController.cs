// <copyright file="UnboundActionsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using Base;
    using MetadataService;
    using MetadataService.Attributes;
    using Persistence.WNP;
    using Services;
    using Services.FilterTransformer;

    /// <summary>
    /// Controller for Unbound OData Actions
    /// </summary>
    [ActionPrefix("Unbound")]
    public class UnboundActionsController : WNPController, IUnboundActionsContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnboundActionsController"/> class.
        /// </summary>
        /// <param name="metadataService">The metadata service.</param>
        /// <param name="dbContext">The database context.</param>
        /// <param name="filterTransformer">The filter transformer.</param>
        /// <param name="convertor">The convertor.</param>
        /// <param name="actionConfigurator">The action configurator.</param>
        public UnboundActionsController(
            IMetadataProvider metadataService,
            WNPDBContext dbContext,
            IFilterTransformer filterTransformer,
            IAutoConvertor convertor,
            IActionConfigurator actionConfigurator)
            : base(metadataService, dbContext, filterTransformer, convertor, actionConfigurator)
        {
        }

        /// <summary>
        /// Test for unbound Action.
        /// </summary>
        /// <param name="mystr">The mystr.</param>
        /// <returns>Same string.</returns>
        [UnboundAction]
        public string Test(
            string mystr)
        {
            return mystr;
        }
    }
}