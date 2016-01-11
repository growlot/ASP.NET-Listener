// //-----------------------------------------------------------------------
// <copyright file="TransactionRegistryController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Routing;
    using ApplicationService;
    using ApplicationService.Commands;
    using ApplicationService.Model;
    using Domain.Listener.Transaction;
    using Newtonsoft.Json;
    using Serilog;

    /// <summary>
    /// Transaction registry controller
    /// </summary>
    public partial class TransactionRegistryController
    {
        private readonly ITransactionService transactionService;
        private readonly IWnpIntegrationService wnpIntegrationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        /// <param name="transactionService">The transaction service.</param>
        /// <param name="wnpService">The WNP integration service.</param>
        public TransactionRegistryController(ListenerODataContext dbctx, ITransactionService transactionService, IWnpIntegrationService wnpService)
        {
            this._dbContext = dbctx;
            this.transactionService = transactionService;
            this.wnpIntegrationService = wnpService;
        }

        /// <summary>
        /// Opens new Listener transaction.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The key of new transaction</returns>
        [HttpPost]
        [ODataRoute("Open")]
        public async Task<IHttpActionResult> Open(ODataActionParameters parameters)
        {
            try
            {
                // string data = (string)this.Request.Properties["ListenerRequestBody"];
                var message = new OpenTransactionCommand
                {
                    CompanyCode = this.CompanyCode,
                    OperationKey = parameters["OperationKey"]?.ToString(),
                    EntityName = parameters["EntityCategory"]?.ToString(),
                    SourceApplicationKey = this.ApplicationKey,
                    Data = parameters["Body"]?.ToString(),
                    User = this.User?.Identity.Name
                };

                return this.Ok(await this.transactionService.Open(message));
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Opens new batch listener transaction.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>The key of new transaction</returns>
        [HttpPost]
        [ODataRoute("Batch")]
        public async Task<IHttpActionResult> Batch(ODataActionParameters parameters)
        {
            try
            {
                var message = new OpenBatchTransactionCommand
                {
                    CompanyCode = this.CompanyCode,
                    SourceApplicationKey = this.ApplicationKey,
                    User = this.User?.Identity.Name,
                    BatchNumber = parameters["BatchNumber"]?.ToString(),
                };

                if (parameters.ContainsKey("Body"))
                {
                    if (parameters["Body"] == null)
                    {
                        throw new InvalidOperationException("Batch body not found");
                    }

                    var body = JsonConvert.DeserializeObject<List<ExpandoObject>>(parameters["Body"]?.ToString());

                    foreach (var jToken in body.Cast<IDictionary<string, object>>())
                    {
                        int p;
                        message.Batch.Add(
                            new BatchTransactionEntry
                            {
                                OperationKey = jToken["OperationKey"]?.ToString(),
                                EntityCategory = jToken["EntityCategory"]?.ToString(),
                                Data = JsonConvert.SerializeObject(jToken),
                                Priority = int.TryParse(jToken["Priority"]?.ToString(), out p) ? p : (int?)null
                            });
                    }
                }

                return this.Ok(await this.transactionService.Open(message));
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        /// <summary>
        /// Opens new batch listener transaction.
        /// </summary>
        /// <param name="batchKey">The batch key.</param>
        /// <returns>The key of new transaction</returns>
        [HttpPost]
        [ODataRoute("BuildBatch")]
        public async Task<IHttpActionResult> BuildBatch([FromBody]string batchKey)
        {
            try
            {
                var openBatchTransactionCommand = await this.wnpIntegrationService.Create(batchKey, this.CompanyCode, this.ApplicationKey, this.User?.Identity.Name);
                return this.Ok(await this.transactionService.Open(openBatchTransactionCommand)); // await Task.WhenAll(records.Select(r => this._transactionService.Open(r)))
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
        public async Task<IHttpActionResult> Process([FromODataUri] Guid key)
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
        public async Task<IHttpActionResult> Retry([FromODataUri] Guid key)
        {
            try
            {
                await this.transactionService.Process(new ProcessTransactionCommand { RecordKey = key, RetryPolicy = RetryPolicyType.Retry });
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
        public async Task<IHttpActionResult> ForceRetry([FromODataUri] Guid key)
        {
            try
            {
                await this.transactionService.Process(new ProcessTransactionCommand { RecordKey = key, RetryPolicy = RetryPolicyType.Force });
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
        public async Task<IHttpActionResult> Succeed([FromODataUri] Guid key)
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
        [HttpPost]
        public async Task<IHttpActionResult> Fail([FromODataUri] Guid key, ODataActionParameters parameters)
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