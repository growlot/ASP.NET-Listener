// //-----------------------------------------------------------------------
// // <copyright file="TransactionController.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApiService
{
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
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;ApiResponseMessage&gt;.</returns>
        [Route("process")]
        [HttpPut]
        public async Task<IHttpActionResult> Process(string id)
        {
            return
                await
                    TryExecuteOperationAsync(
                        context => this._transactionService.Process(new TransactionRequestMessage()));
        }
    }
}