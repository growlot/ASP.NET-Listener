//-----------------------------------------------------------------------
// <copyright file="TransactionalODataBatchHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ODataService.HttpMessageHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.OData.Batch;
    using Persistence;

    /// <summary>
    /// Custom OData batch handler that ensures that changeset is executed in single transaction.
    /// </summary>
    public class TransactionalODataBatchHandler : DefaultODataBatchHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionalODataBatchHandler"/> class.
        /// </summary>
        /// <param name="httpServer">The HTTP server.</param>
        public TransactionalODataBatchHandler(HttpServer httpServer)
            : base(httpServer)
        {
        }

        /// <summary>
        /// Executes the batch request and associates a <see cref="WNPDBContext"/> instance with all the requests of
        /// a single changeset and wraps the execution of the whole changeset within a transaction.
        /// </summary>
        /// <param name="requests">The <see cref="ODataBatchRequestItem"/> instances of this batch request.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> associated with the request.</param>
        /// <returns>The list of responses associated with the batch request.</returns>
        public async override Task<IList<ODataBatchResponseItem>> ExecuteRequestMessagesAsync(
            IEnumerable<ODataBatchRequestItem> requests,
            CancellationToken cancellationToken)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }

            var responses = new List<ODataBatchResponseItem>();
            try
            {
                foreach (var request in requests)
                {
                    // if request is a change set, then execute it in single transaction
                    // else execute it as separate request
                    var changeSet = request as ChangeSetRequestItem;
                    if (changeSet != null)
                    {
                        await this.ExecuteChangeSet(changeSet, responses, cancellationToken);
                    }
                    else
                    {
                        responses.Add(await request.SendRequestAsync(this.Invoker, cancellationToken));
                    }
                }
            }
            catch
            {
                foreach (var response in responses)
                {
                    if (response != null)
                    {
                        response.Dispose();
                    }
                }

                throw;
            }

            return responses;
        }

        /// <summary>
        /// Create a new <see cref="WNPDBContext"/> instance, associate it with each of the requests, start a new
        /// transaction, execute the changeset and then commit or rollback the transaction depending on
        /// whether the responses were all successful or not.
        /// </summary>
        /// <param name="changeSet">The change set.</param>
        /// <param name="responses">The responses.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>The empty task</returns>
        private async Task ExecuteChangeSet(
            ChangeSetRequestItem changeSet,
            IList<ODataBatchResponseItem> responses,
            CancellationToken cancellation)
        {
            ChangeSetResponseItem changeSetResponse;

            ////using (var context = new WNPDBContext("WNPDatabase"))
            ////{
            ////    foreach (HttpRequestMessage request in changeSet.Requests)
            ////    {
            ////        request.SetContext(context);
            ////    }

            ////    using (DbContextTransaction transaction = context.Database.BeginTransaction())
            ////    {
                    changeSetResponse = (ChangeSetResponseItem)await changeSet.SendRequestAsync(this.Invoker, cancellation);
                    responses.Add(changeSetResponse);

                ////    if (changeSetResponse.Responses.All(r => r.IsSuccessStatusCode))
                ////    {
                ////        transaction.Commit();
                ////    }
                ////    else
                ////    {
                ////        transaction.Rollback();
                ////    }
                ////}
            ////}
        }
    }
}
