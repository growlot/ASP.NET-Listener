// //-----------------------------------------------------------------------
// // <copyright file="TransactionController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
    using System.Collections.Specialized;
    using System.Runtime.Remoting.Messaging;
    using System.Threading.Tasks;
    using System.Web.Http;
    using ApplicationService;
    using Communication;

    [RoutePrefix("api/transaction")]
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _transactionService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionController"/> class.
        /// </summary>
        /// <param name="transactionService">The transaction service.</param>
        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="entityCategory">The entity category.</param>
        /// <param name="operationKey">The operation key.</param>
        /// <param name="entityKey">The entity key.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [Route("{entityCategory}('{entityKey}')/{operationKey}()")]
        [HttpPut]
        public async Task<IHttpActionResult> Open(string entityCategory, string operationKey, string entityKey)
        {
            string data = await Request.Content.ReadAsStringAsync();

            var message = new OpenTransactionRequestMessage
            {
                CompanyCode = CompanyCode,
                OperationKey = operationKey,
                SourceApplicationKey = ApplicationKey,
                Data = data,
                User = User?.Identity.Name
            };

            message.Header.Add("PrimaryCategory", entityCategory);
            message.Header.Add("PrimaryKey", entityKey);
            message.Header.Add("Operation", operationKey);

            return
                await
                    TryExecuteOperationAsync(
                        context =>
                            this._transactionService.Open(message));
        }


        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [Route("Transaction('{id}')/Process()")]
        [HttpPost]
        public async Task<IHttpActionResult> Process(string id)
        {
            return
                await
                    TryExecuteOperationAsync(
                        context => this._transactionService.Process(new ProcessTransactionRequestMessage { TransactionKey = id }));
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [Route("success")]
        [HttpPost]
        public async Task<IHttpActionResult> Success(string id)
        {
            return
                await
                    TryExecuteOperationAsync(
                        context => this._transactionService.Success(new TransactionSuccessMessage { TransactionKey = id }));
        }

        /// <summary>
        /// Processes the specified transaction.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [Route("fail")]
        [HttpPost]
        public async Task<IHttpActionResult> Fail(string id, string message, string details)
        {
            return
                await
                    TryExecuteOperationAsync(
                        context => this._transactionService.Failed(new TransactionFailedMessage { TransactionKey = id, Message = message, Details = details }));
        }
    }
}