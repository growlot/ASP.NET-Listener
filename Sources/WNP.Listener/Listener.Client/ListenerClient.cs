﻿using System;
using System.Collections.Generic;

namespace AMSLLC.Listener.Client
{
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using AMSLLC.Listener;
    using AMSLLC.Listener.Persistence.Listener;
    using Exception;
    using Message;
    using Microsoft.OData.Client;
    using Newtonsoft.Json;
    using Serilog;
    using Shared;

    public class ListenerClient : IDisposable
    {
        private readonly Uri _baseUri = null;
        private IListenerProxy _proxy = null;

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
        public ListenerClient(string baseUri, IListenerProxy proxy)
            : this(new Uri(baseUri), proxy)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(Uri baseUri) : this(baseUri, new ListenerProxy(
                new ListenerRequestHeaderDictionary()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="proxy">The proxy.</param>
        public ListenerClient(Uri baseUri, IListenerProxy proxy)
        {
            this._baseUri = baseUri;
            this._proxy = proxy;
        }

        /// <summary>
        /// Processes the device test result.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceTestResult(DeviceTestResultMessage request)
        {
            this.Execute(
                new Uri("listener/Open", UriKind.Relative),
                new DeviceTestResultRequestMessage(request.EquipmentType)
                {
                    EntityKey = request.EquipmentNumber,
                    TestDate = request.TestDate,
                    Owner = request.CompanyId
                });
        }

        /// <summary>
        /// Processes the device update.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceUpdate(
            DeviceUpdateMessage request)
        {
            this.Execute(
                 new Uri("listener/Open", UriKind.Relative),
                 new DeviceUpdateRequestMessage(request.EquipmentType)
                 {
                     EntityKey = request.EquipmentNumber,
                     Owner = request.CompanyId
                 });
        }

        /// <summary>
        /// Processes the batch.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task.</returns>
        public void ProcessBatch(
            BatchAcceptedMessage request)
        {
            this.Execute(new Uri("listener/BuildBatch", UriKind.Relative), request.BatchNumber);
        }

        /// <summary>
        /// Searches the transactions.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns>ICollection&lt;TransactionInfoResponseMessage&gt;.</returns>
        public ICollection<TransactionInfoResponseMessage> SearchTransactions(TransactionFilter filter)
        {
            Log.Logger.Information("Querying transaction with filter {0}", JsonConvert.SerializeObject(filter));
            var container = new Container(this._baseUri);
            var result = (filter == null ? container.TransactionRegistryDetails : container.TransactionRegistryDetails.Where(this.BuildQuery(filter))).ToList();
            return result.Select(
                s => new TransactionInfoResponseMessage
                {
                    CreatedDate = s.CreatedDateTime.UtcDateTime,
                    Debug = s.Details,
                    EventStartDate = s.StartDate.HasValue ? s.StartDate.Value.UtcDateTime : (DateTime?)null,
                    EntityCategory = s.EntityCategory,
                    EntityKey = s.EntityKey,
                    Message = s.Message,
                    OperationKey = s.OperationName,
                    TransactionStatus = (TransactionStatusType)s.TransactionStatusId
                }).ToList();
        }

        private Expression<Func<TransactionRegistryViewEntity, bool>> BuildQuery(TransactionFilter filter)
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

                if (filter.StatusTypes.Any())
                {
                    baseExpression =
                        baseExpression.Compose(
                            f => filter.StatusTypes.Contains((TransactionStatusType)f.TransactionStatusId),
                            Expression.AndAlso);
                }
            }
            return baseExpression;
        }



        //private string BuildTransactionFilterQuery(
        //    TransactionFilter filter)
        //{
        //    /*
        //    public string EntityCategory { get; set; }
        //public string EntityKey { get; set; }
        //public string BatchNumber { get; set; }
        //public List<TransactionStatusType> StatusTypes { get; } = new List<TransactionStatusType>();
        //public DateTime? TransactionDate { get; set; }
        //    */

        //    List<string> returnValue = new List<string>();

        //    if (!string.IsNullOrWhiteSpace(filter.EntityCategory))
        //    {
        //        returnValue.Add($"EntityCategory eq '{filter.EntityCategory}'");
        //    }

        //    if (!string.IsNullOrWhiteSpace(filter.EntityKey))
        //    {
        //        returnValue.Add($"EntityKey eq '{filter.EntityKey}'");
        //    }

        //    if (!string.IsNullOrWhiteSpace(filter.BatchNumber))
        //    {
        //        returnValue.Add($"BatchNumber eq '{filter.BatchNumber}'");
        //    }

        //    if (filter.TransactionDate.HasValue)
        //    {
        //        returnValue.Add($"CreatedDateTime eq '{filter.TransactionDate.Value}'");
        //    }

        //    return string.Join("&", returnValue);
        //}

        private void Execute(
            Uri relativeUri,
            object message)
        {
            string transactionKey;
            string response;
            try
            {
                response = this.OpenTask(relativeUri, message);

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
                this.ProcessTask(transactionKey);
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
                        return false;
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

        public virtual string OpenTask(
            Uri relativeUri,
            object message)
        {
            var openTask = this._proxy.OpenAsync(
                new Uri(this._baseUri, relativeUri),
                message);

            openTask.Wait();
            return openTask.Result;
        }

        public virtual void FailTransaction(
            string transactionKey,
            string excMessage,
            string stacktrace)
        {
            var t =
                 this._proxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Fail()", UriKind.Relative)),
                    JsonConvert.SerializeObject(
                        new ProcessingFailedRequestMessage
                        {
                            Message = excMessage,
                            Details = stacktrace
                        }));
            t.Wait();
        }

        public virtual void ProcessTask(
            string transactionKey)
        {
            var openedTask =
                 this._proxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Process()", UriKind.Relative)),
                    null);

            openedTask.Wait();
        }

        public virtual void SucceedTransaction(
            string key)
        {
            var succeedTask =
                 this._proxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({key})/AMSLLC.Listener.Succeed()", UriKind.Relative)),
                    null);

            succeedTask.Wait();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ListenerClient()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._proxy.Dispose();
            }
        }
    }
}
