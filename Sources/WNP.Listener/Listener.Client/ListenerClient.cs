// <copyright file="ListenerClient.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Client.Exception;
    using AMSLLC.Listener.Client.Message;
    using AMSLLC.Listener.Persistence.Listener;
    using AMSLLC.Listener.Shared;
    using Microsoft.OData.Client;
    using Newtonsoft.Json;
    using Serilog;

    /// <summary>
    /// Allows to interact with Listener server.
    /// </summary>
    public class ListenerClient : IDisposable
    {
        private readonly Uri baseUri;
        private readonly ListenerContainer container;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(string baseUri)
            : this(new Uri(baseUri))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(Uri baseUri)
        {
            this.baseUri = baseUri;
            this.container = new ListenerContainer(this.baseUri);
            this.container.BuildingRequest += this.BuildingRequest;
        }

        /// <summary>
        ///     Finalizes an instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        ~ListenerClient()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Processes the device test result.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceTestResult(
            DeviceTestResultMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestMessage = new DeviceTestResultRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                TestDate = request.TestDate,
                Owner = request.CompanyId
            };

            this.Execute(() => this.OpenTransaction(requestMessage));
        }

        /// <summary>
        ///     Processes the device update.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceUpdate(
            DeviceUpdateMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestMessage = new DeviceUpdateRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                Owner = request.CompanyId
            };

            this.Execute(() => this.OpenTransaction(requestMessage));
        }

        /// <summary>
        ///     Processes the batch.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessBatch(
            BatchAcceptedMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            this.Execute(() => this.OpenBatch(request.BatchNumber));
        }

        /// <summary>
        ///     Publish the tracking information.
        /// </summary>
        /// <param name="request">The request.</param>
        public void PublishTracking(
            ChangeDeviceStatusMessage request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var requestMessage = new ChangeDeviceStatusRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                Owner = request.CompanyId,
                CreatedDate = request.CreatedDate
            };

            this.Execute(() => this.OpenTransaction(requestMessage));
        }

        /// <summary>
        ///     Opens the transaction asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>The unique transaction key.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Direct return of the async method for simplicity")]
        public virtual Task<IEnumerable<Guid>> OpenTransactionAsync<TRequest>(TRequest request)
            where TRequest : BaseListenerRequestMessage
        {
            DataServiceActionQuery<Guid> result = this.container.Open(
                request.EntityCategory,
                request.OperationKey,
                JsonConvert.SerializeObject(request));

            return result.ExecuteAsync();
        }

        /// <summary>
        ///     Process the transaction asynchronously.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <returns>
        ///     Task.
        /// </returns>
        public virtual Task ProcessTransactionAsync(
            Guid transactionKey)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", transactionKey
                    }
                }).Process();
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(
                            x =>
                            {
                                Log.Logger.Error(
                                    "An error has occured while processing transaction {0}",
                                    JsonConvert.SerializeObject(t.Exception));
                                return true;
                            });
                        throw new FailedToProcessTransactionException();
                    }
                });
        }

        /// <summary>
        ///     Succeed the transaction asynchronously.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <returns>
        ///     Task.
        /// </returns>
        public Task SucceedTransactionAsync(
            Guid transactionKey)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", transactionKey
                    }
                }).Succeed();
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(
                            x =>
                            {
                                Log.Logger.Error(
                                    "An error has occured while succeeding transaction {0}",
                                    JsonConvert.SerializeObject(t.Exception));
                                return true;
                            });
                        throw new FailedToSucceedTransactionException();
                    }
                });
        }

        /// <summary>
        ///     Tasks the fail transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="exceptionMessage">The exception message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>Task.</returns>
        public Task FailTransactionAsync(
            string transactionKey,
            string exceptionMessage,
            string stackTrace)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", Guid.Parse(transactionKey)
                    }
                }).Fail(exceptionMessage, stackTrace);
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(
                            x =>
                            {
                                Log.Logger.Error(
                                    "An error has occured while failing transaction {0}",
                                    JsonConvert.SerializeObject(t.Exception));
                                return true;
                            });
                        throw new FailedToFailTransactionException();
                    }
                });
        }

        /// <summary>
        ///     Searches the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>ICollection&lt;TransactionInfoResponseMessage&gt;.</returns>
        public ICollection<TransactionInfoResponseMessage> SearchTransactions(
            TransactionFilter filter)
        {
            Log.Logger.Debug("Querying transaction with filter {0}", JsonConvert.SerializeObject(filter));
            Expression<Func<TransactionRegistryViewEntity, bool>> filterExpression = null;
            if (filter != null)
            {
                filterExpression = BuildQuery(filter);
                Log.Debug("Applying filter: {0}", filterExpression);
            }

            List<TransactionRegistryViewEntity> result =
                (filterExpression == null
                    ? this.container.TransactionRegistryDetails
                    : this.container.TransactionRegistryDetails.Where(filterExpression)).ToList();
            Log.Debug("Data requested");
            Log.Debug("Received {0} records", result.Count);
            return result.Select(
                s => new TransactionInfoResponseMessage
                {
                    CreatedDate = s.CreatedDateTime.UtcDateTime,
                    Debug = s.Details,
                    EventStartDate = s.StartDate?.UtcDateTime,
                    EntityCategory = s.EntityCategory,
                    EntityKey = s.EntityKey,
                    Message = s.Message,
                    OperationKey = s.OperationName,
                    TransactionStatus = (TransactionStatusType)s.TransactionStatusId,
                    TransactionSource = s.ApplicationName
                }).ToList();
        }

        /// <summary>
        ///     Opens the transaction.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The transaction key.</returns>
        public virtual Guid[] OpenTransaction(
            BaseListenerRequestMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            DataServiceActionQuery<Guid> result = this.container.Open(
                message.EntityCategory,
                message.OperationKey,
                JsonConvert.SerializeObject(message));
            return result.Execute().ToArray();
        }

        /// <summary>
        ///     Opens the transaction.
        /// </summary>
        /// <param name="batchKey">The batch key.</param>
        /// <returns>The transaction key.</returns>
        public virtual Guid[] OpenBatch(
            string batchKey)
        {
            DataServiceActionQuery<Guid> result = this.container.BuildBatch(batchKey);

            return result.Execute().ToArray(); // .GetEnumerator();
        }

        /// <summary>
        ///     Fails the transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="errorMessage">The error message for user.</param>
        /// <param name="errorDetails">The error details.</param>
        public virtual void FailTransaction(
            Guid transactionKey,
            string errorMessage,
            string errorDetails)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", transactionKey
                    }
                }).Fail(errorMessage, errorDetails);
            OperationResponse response = query.Execute();
            if (response.StatusCode == 500 || response.Error != null)
            {
                throw new FailedToFailTransactionException(response.Error?.Message, response.Error);
            }
        }

        /// <summary>
        ///     Processes the transaction. Transaction must be opened before it can be processed.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        public virtual void ProcessTransaction(
            Guid transactionKey)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", transactionKey
                    }
                }).Process();

            OperationResponse response = query.Execute();
            if (response.StatusCode == 500 || response.Error != null)
            {
                throw new FailedToProcessTransactionException(response.Error?.Message, response.Error);
            }
        }

        /// <summary>
        ///     Succeeds the transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        public virtual void SucceedTransaction(
            Guid transactionKey)
        {
            DataServiceActionQuery query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", transactionKey
                    }
                }).Succeed();

            OperationResponse response = query.Execute();
            if (response.StatusCode == 500 || response.Error != null)
            {
                throw new FailedToSucceedTransactionException(response.Error?.Message, response.Error);
            }
        }

        /// <summary>
        ///     Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.
        /// </param>
        protected virtual void Dispose(
            bool disposing)
        {
            if (disposing)
            {
            }
        }

        private static Expression<Func<TransactionRegistryViewEntity, bool>> BuildQuery(
            TransactionFilter filter)
        {
            Expression<Func<TransactionRegistryViewEntity, bool>> baseExpression = o => true;
            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.EntityCategory))
                {
                    baseExpression = baseExpression.Compose(
                        f => f.EntityCategory == filter.EntityCategory,
                        Expression.AndAlso);
                }

                if (!string.IsNullOrWhiteSpace(filter.EntityKey))
                {
                    baseExpression = baseExpression.Compose(f => f.EntityKey == filter.EntityKey, Expression.AndAlso);
                }

                if (!string.IsNullOrWhiteSpace(filter.OperationName))
                {
                    baseExpression = baseExpression.Compose(f => f.OperationName == filter.OperationName, Expression.AndAlso);
                }

                if (!string.IsNullOrWhiteSpace(filter.BatchNumber))
                {
                    baseExpression = baseExpression.Compose(
                        f => f.BatchNumber == filter.BatchNumber,
                        Expression.AndAlso);
                }

                if (filter.TransactionDate.HasValue)
                {
                    baseExpression = baseExpression.Compose(
                        f => f.StartDate == filter.TransactionDate,
                        Expression.AndAlso);
                }

                int[] str = filter.StatusTypes.Cast<int>().ToArray();
                if (str.Any())
                {
                    Expression<Func<TransactionRegistryViewEntity, bool>> inner = o => false;
                    foreach (int i in str)
                    {
                        inner = inner.Compose(f => f.TransactionStatusId == i, Expression.OrElse);
                    }

                    baseExpression = baseExpression.Compose(inner, Expression.AndAlso);
                }
            }

            return baseExpression;
        }

        private void Execute(
            Func<IEnumerable<Guid>> opener)
        {
            IEnumerable<Guid> transactionKeys;
            try
            {
                transactionKeys = opener();

                Log.Logger.Information("Opened transaction {0}", transactionKeys);
            }
            catch (System.Exception exc)
            {
                throw new FailedToOpenTransactionException(exc.Message, exc);
            }

            foreach (Guid transactionKey in transactionKeys)
            {
                try
                {
                    this.ProcessTransaction(transactionKey);
                    Log.Logger.Information("Processed transaction {0}", transactionKey);
                }
                catch (AggregateException exc)
                {
                    exc.Handle(
                        x =>
                        {
                            Log.Logger.Error(x, "Failed to process transaction");
                            string excMessage = x.Message;
                            string stacktrace = x.StackTrace;
                            this.FailTransaction(transactionKey, excMessage, stacktrace);
                            return true;
                        });
                    throw new FailedToProcessTransactionException(exc.Message, exc);
                }
                catch (System.Exception exc)
                {
                    Log.Logger.Error(exc, "Failed to process transaction");
                    string excMessage = exc.Message;
                    string stacktrace = exc.StackTrace;
                    this.FailTransaction(transactionKey, excMessage, stacktrace);
                    throw new FailedToProcessTransactionException(exc.Message, exc);
                }

                try
                {
                    this.SucceedTransaction(transactionKey);
                    Log.Logger.Information("Succeeded transaction {0}", transactionKey);
                }
                catch (System.Exception exc)
                {
                    Log.Logger.Error(exc, "Failed to succeed transaction");
                    throw new FailedToSucceedTransactionException(exc.Message, exc);
                }
            }
        }

        private void BuildingRequest(
            object sender,
            BuildingRequestEventArgs e)
        {
            var headers = new ListenerRequestHeaderDictionary();
            foreach (KeyValuePair<string, string> keyValuePair in headers)
            {
                e.Headers.Add(keyValuePair.Key, keyValuePair.Value);
            }
        }
    }
}