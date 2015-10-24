// <copyright file="TransactionController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using ApplicationService;
    using ApplicationService.Commands;
    using Persistence.Listener;
    using Serilog;

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.Equal)]
    public class TransactionController : ODataController
    {
        private readonly ListenerODataContext dbContext;
        private readonly ITransactionService transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        /// <param name="transactionService">The transaction service.</param>
        public TransactionController(ListenerODataContext dbctx, ITransactionService transactionService)
        {
            this.dbContext = dbctx;
            this.transactionService = transactionService;
        }

        public string CompanyCode => this.Request.Headers.GetValues("AMS-Company").FirstOrDefault();

        public string ApplicationKey => this.Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        [CLSCompliant(false)]
        public IQueryable<TransactionRegistryEntity> Get()
        {
            try
            {
                return this.dbContext.TransactionRegistry.AsQueryable();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        public IHttpActionResult Get([FromODataUri] string key)
        {
            try
            {
                var result = this.dbContext.TransactionRegistry.Where(s => s.RecordKey == key);
                return this.Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [HttpPost]
        public async Task<IHttpActionResult> Process([FromODataUri] string key)
        {
            try
            {
                await this.transactionService.Process(new ProcessTransactionCommand { RecordKey = key });
                return this.Ok();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [HttpPost]
        public async Task<IHttpActionResult> Succeed([FromODataUri] string key)
        {
            try
            {
                await this.transactionService.Success(new SucceedTransactionCommand() { RecordKey = key });
                return this.Ok();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        // [ODataRoute("(Message={message},Details={details})")]
        [HttpPost]
        public async Task<IHttpActionResult> Fail([FromODataUri] string key, ODataActionParameters parameters)
        {
            try
            {
                await
                    this.transactionService.Failed(new FailTransactionCommand()
                    {
                        RecordKey = key,
                        Details = parameters.ContainsKey("Details") ? parameters["Details"].ToString() : null,
                        Message = parameters.ContainsKey("Message") ? parameters["Message"].ToString() : null
                    });
                return this.Ok();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }
}