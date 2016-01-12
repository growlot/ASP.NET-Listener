// <copyright file="TransactionRegistryDetailsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using System.Web.OData;
    using Core;
    using Serilog;

    /// <summary>
    /// Transaction registry details controller customization.
    /// </summary>
    public partial class TransactionRegistryDetailsController
    {
        private readonly IDateTimeProvider dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryDetailsController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public TransactionRegistryDetailsController(ListenerODataContext dbctx, IDateTimeProvider dateTimeProvider)
        {
            this._dbContext = dbctx;
            this.dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Counts the number of transactions by status.
        /// </summary>
        /// <param name="statusTypes">The status types to include in the count.</param>
        /// <returns>The number of transactions with specified statuses.</returns>
        [HttpGet]
        public IHttpActionResult CountByStatus([FromODataUri]IEnumerable<int> statusTypes)
        {
            try
            {
                var sinceDate = this.dateTimeProvider.Now().AddDays(-30);

                // var dt = DateTime.Parse(sinceDate);
                var result = this._dbContext.TransactionRegistryDetails.Count(s => statusTypes.Contains(s.TransactionStatusId) && (s.CreatedDateTime >= sinceDate || s.UpdatedDateTime >= sinceDate) && s.CompanyCode == this.CompanyCode && s.ApplicationKey == this.ApplicationKey);

                // var result = this._dbContext.TransactionRegistryDetails.Count();
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }
}
