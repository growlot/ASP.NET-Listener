// //-----------------------------------------------------------------------
// // <copyright file="ListenerController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System.Linq;
    using System.Web.Http;
    using System.Web.Http.OData;
    using System.Web.Http.OData.Query;
    using Persistence.Listener;

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
    public class TransactionRegistryController : ODataController
    {
        private readonly ListenerODataContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryController"/> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        public TransactionRegistryController(ListenerODataContext dbctx)
        {
            _dbContext = dbctx;
        }

        public IQueryable<TransactionRegistryEntity> Get()
        {
            return _dbContext.TransactionRegistry.AsQueryable();
        }
    }
}