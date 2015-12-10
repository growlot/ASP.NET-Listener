// <copyright file="TransactionRegistryDetailsController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using Core;
    using DbContext;
    using Persistence.Listener;
    using Serilog;
    using Shared;

    public partial class TransactionRegistryDetailsController
    {

        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryDetailsController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public TransactionRegistryDetailsController(ListenerODataContext dbctx, IDateTimeProvider dateTimeProvider)
        {
            this._dbContext = dbctx;
            this._dateTimeProvider = dateTimeProvider;
        }

        [HttpGet]
        public IHttpActionResult CountByStatus([FromODataUri]IEnumerable<int> statusTypes)//[FromODataUri]IEnumerable<int> statusTypes, [FromODataUri] string sinceDate
        {
            try
            {
                var sinceDate = this._dateTimeProvider.Now().AddDays(-30);
                //var dt = DateTime.Parse(sinceDate);
                var result = this._dbContext.TransactionRegistryDetails.Count(s => statusTypes.Contains(s.TransactionStatusId) && (s.CreatedDateTime >= sinceDate || s.UpdatedDateTime >= sinceDate) && s.CompanyCode == this.CompanyCode && s.ApplicationKey == this.ApplicationKey);
                //var result = this._dbContext.TransactionRegistryDetails.Count();
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
