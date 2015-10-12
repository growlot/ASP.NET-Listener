// //-----------------------------------------------------------------------
// // <copyright file="ListenerController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ODataService.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData;
    using System.Web.OData.Query;
    using System.Web.OData.Routing;
    using ApplicationService;
    using Communication;
    using Newtonsoft.Json;
    using Persistence.Listener;

    [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
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
            return _dbContext.TransactionRegistry;
        }

        public IHttpActionResult Get([FromODataUri] int key)
        {
            var result = _dbContext.TransactionRegistry.Where(s => s.TransactionId == key);
            return Ok(result);
        }

        [HttpPost]
        [ODataRoute("Open")]//(entityCategory={entityCategory},operationKey={operationKey},entityKey={entityKey})
        public async Task<IHttpActionResult> Open(ODataActionParameters parameters)//[FromODataUri]string entityCategory, [FromODataUri]string operationKey, [FromODataUri]string entityKey
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
    }
}