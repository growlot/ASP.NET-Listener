// //-----------------------------------------------------------------------
// // <copyright file="ListenerController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using ApplicationService;
    using Communication;
    using Persistence.Listener;
    using Serilog;

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All, AllowedLogicalOperators = AllowedLogicalOperators.Equal)]
    public class TransactionRegistryController : ODataController
    {
        private readonly ListenerODataContext _dbContext;
        private readonly ITransactionService _transactionService;

        public string CompanyCode => Request.Headers.GetValues("AMS-Company").FirstOrDefault();
        public string ApplicationKey => Request.Headers.GetValues("AMS-Application").FirstOrDefault();

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRegistryController" /> class.
        /// </summary>
        /// <param name="dbctx">The db context.</param>
        /// <param name="transactionService">The transaction service.</param>
        public TransactionRegistryController(ListenerODataContext dbctx, ITransactionService transactionService)
        {
            _dbContext = dbctx;
            this._transactionService = transactionService;
        }

        public IQueryable<TransactionRegistryEntity> Get()
        {
            try
            {
                return _dbContext.TransactionRegistry.AsQueryable();
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
                var result = _dbContext.TransactionRegistry.Where(s => s.RecordKey == key);
                return Ok(result);
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }

        [HttpPost]
        [ODataRoute("Open")]//(entityCategory={entityCategory},operationKey={operationKey},entityKey={entityKey})
        public async Task<IHttpActionResult> Open(ODataActionParameters parameters)//[FromODataUri]string entityCategory, [FromODataUri]string operationKey, [FromODataUri]string entityKey
        {
            try
            {
                string data = (string)Request.Properties["ListenerRequestBody"];

                var message = new OpenTransactionRequestMessage
                {
                    CompanyCode = CompanyCode,
                    OperationKey = parameters["OperationKey"]?.ToString(),
                    SourceApplicationKey = ApplicationKey,
                    Data = data,
                    User = User?.Identity.Name
                };

                message.Header.Add("PrimaryCategory", parameters["EntityCategory"]?.ToString());
                message.Header.Add("PrimaryKey", parameters["EntityKey"]?.ToString());
                message.Header.Add("Operation", parameters["OperationKey"]?.ToString());

                return Ok(await this._transactionService.Open(message));
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
                await this._transactionService.Process(new ProcessTransactionRequestMessage { RecordKey = key });
                return Ok();
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
                await this._transactionService.Success(new TransactionSuccessMessage() { RecordKey = key });
                return Ok();
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
        //[ODataRoute("(Message={message},Details={details})")]
        public async Task<IHttpActionResult> Fail([FromODataUri] string key, ODataActionParameters parameters)
        {
            try
            {
                await
                    this._transactionService.Failed(new TransactionFailedMessage()
                    {
                        RecordKey = key,
                        Details = parameters.ContainsKey("Details") ? parameters["Details"].ToString() : null,
                        Message = parameters.ContainsKey("Message") ? parameters["Message"].ToString() : null
                    });
                return Ok();
            }
            catch (Exception exc)
            {
                Log.Error(exc, "Operation Failed");
                throw;
            }
        }
    }
}