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
    using AMSLLC.Listener;
    using AMSLLC.Listener.Persistence.Listener;
    using Exception;
    using Message;
    using Newtonsoft.Json;
    using Serilog;
    using Shared;

    /// <summary>
    /// Allows to interact with Listener server.
    /// </summary>
    public class ListenerClient : IDisposable
    {
        private readonly Uri baseUri = null;
        private Container container = null;
        private IListenerProxy proxy = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(string baseUri)
            : this(baseUri, new ListenerProxy(new ListenerRequestHeaderDictionary()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="proxy">The proxy.</param>
        public ListenerClient(
            string baseUri,
            IListenerProxy proxy)
            : this(new Uri(baseUri), proxy)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(Uri baseUri)
            : this(baseUri, new ListenerProxy(new ListenerRequestHeaderDictionary()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="proxy">The proxy.</param>
        public ListenerClient(
            Uri baseUri,
            IListenerProxy proxy)
        {
            this.baseUri = baseUri;
            this.proxy = proxy;
            this.container = new Container(this.baseUri);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        ~ListenerClient()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Processes the device test result.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceTestResult(
            DeviceTestResultMessage request)
        {
            var requestMessage = new DeviceTestResultRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                TestDate = request.TestDate,
                Owner = request.CompanyId
            };

            this.Execute(
                new Uri("listener/Open", UriKind.Relative),
                new OpenTransactionRequestWrapper<DeviceTestResultRequestMessage>(requestMessage));
        }

        /// <summary>
        /// Processes the device update.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceUpdate(
            DeviceUpdateMessage request)
        {
            var requestMessage = new DeviceUpdateRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                Owner = request.CompanyId
            };

            this.Execute(
                new Uri("listener/Open", UriKind.Relative),
                new OpenTransactionRequestWrapper<DeviceUpdateRequestMessage>(requestMessage));
        }

        /// <summary>
        /// Processes the batch.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessBatch(
            BatchAcceptedMessage request)
        {
            this.Execute(new Uri("listener/BuildBatch", UriKind.Relative), request.BatchNumber);
        }

        /// <summary>
        /// Publish the tracking information.
        /// </summary>
        /// <param name="request">The request.</param>
        public void PublishTracking(
            ChangeDeviceStatusMessage request)
        {
            var requestMessage = new ChangeDeviceStatusRequestMessage(request.EquipmentType)
            {
                EntityKey = request.EquipmentNumber,
                Owner = request.CompanyId,
                CreatedDate = request.CreatedDate
            };

            this.Execute(
                new Uri("listener/Open", UriKind.Relative),
                new OpenTransactionRequestWrapper<ChangeDeviceStatusRequestMessage>(requestMessage));
        }

        /// <summary>
        /// Opens the transaction asynchronously.
        /// </summary>
        /// <typeparam name="TRequest">The type of the request.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns>The unique transaction key.</returns>
        public Task<string> OpenTransactionAsync<TRequest>(TRequest request)
            where TRequest : BaseListenerRequestMessage
        {
            return this.proxy.OpenAsync(new Uri(this.baseUri, new Uri("listener/Open", UriKind.Relative)), request);
        }

        /// <summary>
        /// Process the transaction asynchronously.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <returns>
        /// Task.
        /// </returns>
        public Task ProcessTransactionAsync(string transactionKey)
        {
            var query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", Guid.Parse(transactionKey)
                    }
                }).Process();
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(x =>
                        {
                            Log.Logger.Error("An error has occured while processing transaction {0}", JsonConvert.SerializeObject(t.Exception));
                            return true;
                        });
                        throw new FailedToProcessTransactionException();
                    }
                });
        }

        /// <summary>
        /// Succeed the transaction asynchronously.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <returns>
        /// Task.
        /// </returns>
        public Task SucceedTransactionAsync(string transactionKey)
        {
            var query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", Guid.Parse(transactionKey)
                    }
                }).Succeed();
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(x =>
                        {
                            Log.Logger.Error("An error has occured while succeeding transaction {0}", JsonConvert.SerializeObject(t.Exception));
                            return true;
                        });
                        throw new FailedToSucceedTransactionException();
                    }
                });
        }

        /// <summary>
        /// Tasks the fail transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="excMessage">The exc message.</param>
        /// <param name="stackTrace">The stack trace.</param>
        /// <returns>Task.</returns>
        public Task FailTransactionAsync(
            string transactionKey,
            string excMessage,
            string stackTrace)
        {
            var query = this.container.TransactionRegistry.ByKey(
                new Dictionary<string, object>
                {
                    {
                        "RecordKey", Guid.Parse(transactionKey)
                    }
                }).Fail(excMessage, stackTrace);
            return query.ExecuteAsync().ContinueWith(
                t =>
                {
                    if (t.Exception != null)
                    {
                        t.Exception.Handle(x =>
                        {
                            Log.Logger.Error("An error has occured while failing transaction {0}", JsonConvert.SerializeObject(t.Exception));
                            return true;
                        });
                        throw new FailedToFailTransaction();
                    }
                });
        }

        /// <summary>
        /// Searches the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>ICollection&lt;TransactionInfoResponseMessage&gt;.</returns>
        public ICollection<TransactionInfoResponseMessage> SearchTransactions(
            TransactionFilter filter)
        {
            Log.Logger.Information("Querying transaction with filter {0}", JsonConvert.SerializeObject(filter));
            var result =
                (filter == null
                    ? this.container.TransactionRegistryDetails
                    : this.container.TransactionRegistryDetails.Where(this.BuildQuery(filter))).ToList();
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
        /// Opens the transaction.
        /// </summary>
        /// <param name="relativeUri">The relative URI.</param>
        /// <param name="message">The message.</param>
        /// <returns>The transaction key.</returns>
        public virtual string OpenTransaction(Uri relativeUri, object message)
        {
            var task = this.proxy.OpenAsync(new Uri(this.baseUri, relativeUri), message);

            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// Fails the transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        /// <param name="errorMessage">The error message for user.</param>
        /// <param name="errorDetails">The error details.</param>
        public virtual void FailTransaction(
                    string transactionKey,
                    string errorMessage,
                    string errorDetails)
        {
            var t =
                this.proxy.OpenAsync(
                    new Uri(
                        this.baseUri,
                        new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Fail()", UriKind.Relative)),
                    JsonConvert.SerializeObject(
                        new ProcessingFailedRequestMessage
                        {
                            Message = errorMessage,
                            Details = errorDetails
                        }));
            t.Wait();
        }

        /// <summary>
        /// Processes the transaction. Transaction must be opened before it can be processed.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        public virtual void ProcessTransaction(string transactionKey)
        {
            var openedTask =
                this.proxy.OpenAsync(
                    new Uri(
                        this.baseUri,
                        new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Process()", UriKind.Relative)),
                    null);

            openedTask.Wait();
        }

        /// <summary>
        /// Succeeds the transaction.
        /// </summary>
        /// <param name="transactionKey">The transaction key.</param>
        public virtual void SucceedTransaction(string transactionKey)
        {
            var succeedTask =
                this.proxy.OpenAsync(
                    new Uri(
                        this.baseUri,
                        new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Succeed()", UriKind.Relative)),
                    null);

            succeedTask.Wait();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        ///   <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(
                    bool disposing)
        {
            if (disposing)
            {
                this.proxy.Dispose();
            }
        }

        private Expression<Func<TransactionRegistryViewEntity, bool>> BuildQuery(
            TransactionFilter filter)
        {
            Expression<Func<TransactionRegistryViewEntity, bool>> baseExpression = (o) => true;
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

                var str = filter.StatusTypes.Select(s => (int)s);
                if (str.Any())
                {
                    Expression<Func<TransactionRegistryViewEntity, bool>> inner = (o) => false;
                    foreach (int i in str)
                    {
                        inner = inner.Compose(f => f.TransactionStatusId == i, Expression.OrElse);
                    }

                    baseExpression = baseExpression.Compose(inner, Expression.AndAlso);
                }
            }

            return baseExpression;
        }

        private void Execute(Uri relativeUri,  object message)
        {
            string transactionKey;
            string response;
            try
            {
                response = this.OpenTransaction(relativeUri, message);

                Log.Logger.Information("Opened transaction {0}", response);
            }
            catch (System.Exception exc)
            {
                throw new FailedToOpenTransactionException(exc.Message, exc);
            }

            var responseExpando =
                JsonConvert.DeserializeObject<ExpandoObject>(response) as IDictionary<string, object>;
            transactionKey = responseExpando["value"]?.ToString();
            Log.Logger.Information("Using transactionKey {0}", transactionKey);
            try
            {
                this.ProcessTransaction(transactionKey);
                Log.Logger.Information("Processed transaction {0}", transactionKey);
            }
            catch (AggregateException exc)
            {
                exc.Handle(
                    (x) =>
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
}