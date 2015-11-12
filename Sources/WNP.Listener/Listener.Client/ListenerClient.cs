using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client
{
    using System.Dynamic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using Exception;
    using Message;
    using Newtonsoft.Json;
    using Serilog;

    public class ListenerClient
    {
        private readonly Uri _baseUri = null;
        private IListenerProxy _proxy = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient" /> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(string baseUri)
            : this(baseUri, new ListenerProxy())
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
        public ListenerClient(Uri baseUri) : this(baseUri, new ListenerProxy())
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
            this.Execute(new Uri("listener/BuildBatch", UriKind.Relative), new BatchAcceptedRequestMessage() { BatchNumber = request.BatchNumber });
        }

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
                message,
                new ListenerRequestHeaderDictionary());

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
                        }),
                    new ListenerRequestHeaderDictionary());
            t.Wait();
        }

        public virtual void ProcessTask(
            string transactionKey)
        {
            var openedTask =
                 this._proxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Process()", UriKind.Relative)),
                    null,
                    new ListenerRequestHeaderDictionary());

            openedTask.Wait();
        }

        public virtual void SucceedTransaction(
            string key)
        {
            var succeedTask =
                 this._proxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({key})/AMSLLC.Listener.Succeed()", UriKind.Relative)),
                    null,
                    new ListenerRequestHeaderDictionary());

            succeedTask.Wait();
        }
    }
}
