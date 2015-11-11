using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client
{
    using System.Threading.Tasks;
    using Message;
    using Newtonsoft.Json;
    using Serilog;

    public class ListenerClient
    {
        private readonly Uri _baseUri = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(string baseUri)
            : this(new Uri(baseUri))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClient"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public ListenerClient(Uri baseUri)
        {
            this._baseUri = baseUri;
        }

        /// <summary>
        /// Processes the device test result.
        /// </summary>
        /// <param name="request">The request.</param>
        public void ProcessDeviceTestResult(DeviceTestResultMessage request)
        {
            this.Execute(
                new Uri("/Open"),
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
                new Uri("/Open"),
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
            this.Execute(new Uri("/OpenBatch"), new BatchAcceptedRequestMessage() {BatchNumber = request.BatchNumber});
        }

        private void Execute(
            Uri relativeUri,
            object message)
        {
            string transactionKey = this.OpenTask(relativeUri, message);

            try
            {
                this.ProcessTask(transactionKey);
            }
            catch (AggregateException exc)
            {
                exc.Handle((x) =>
                {
                    Log.Logger.Error(x, "Failed to process transaction");
                    string excMessage = x.Message;
                    string stacktrace = x.StackTrace;
                    this.FailTransaction(transactionKey, excMessage, stacktrace);
                    return false;
                });
            }

            this.SucceedTransaction(transactionKey);
        }

        protected virtual string OpenTask(
            Uri relativeUri,
            object message)
        {
            var openTask = GenericProxy.OpenAsync(
                new Uri(this._baseUri, relativeUri),
                message,
                new ListenerRequestHeaderDictionary());

            openTask.Wait();
            return openTask.Result;
        }

        protected virtual void FailTransaction(
            string transactionKey,
            string excMessage,
            string stacktrace)
        {
            var t =
                GenericProxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"listener/TransactionRegistry({transactionKey})/AMSLLC.Listener.Fail()")),
                    JsonConvert.SerializeObject(
                        new ProcessingFailedRequestMessage
                        {
                            Message = excMessage,
                            Details = stacktrace
                        }),
                    new ListenerRequestHeaderDictionary());
            t.Wait();
        }

        protected virtual void ProcessTask(
            string transactionKey)
        {
            var openedTask =
                GenericProxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"/TransactionRegistry({transactionKey})/AMSLLC.Listener.Process()")),
                    null,
                    new ListenerRequestHeaderDictionary());

            openedTask.Wait();
        }

        protected virtual void SucceedTransaction(
            string key)
        {
            var succeedTask =
                GenericProxy.OpenAsync(
                    new Uri(this._baseUri, new Uri($"/TransactionRegistry({key})/AMSLLC.Listener.Succeed()")),
                    null,
                    new ListenerRequestHeaderDictionary());

            succeedTask.Wait();
        }
    }
}
