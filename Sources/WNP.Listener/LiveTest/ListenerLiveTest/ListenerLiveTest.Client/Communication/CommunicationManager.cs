using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Communication
{
    using System.Configuration;
    using System.Dynamic;
    using System.Net.Http;
    using System.Threading;
    using AMSLLC.Listener.Client;
    using AMSLLC.Listener.Client.Message;
    using Newtonsoft.Json;
    using Serilog;

    public class CommunicationManager
    {
        public int ParallelRequests { get; }

        public CommunicationManager(int parallelRequests)
        {
            ParallelRequests = parallelRequests;
        }

        public async Task Run(Uri uri, BaseListenerRequestMessage[] data)
        {
            int counter = 0;
            int processingCounter = 0;
            List<Task> tasks = new List<Task>();
            foreach (var o in data)
            {
                var t1 = Task.Run(() => Interlocked.Increment(ref counter)).ContinueWith(t => OpenTransaction(uri, o)).Unwrap();




                var t3 = t1.ContinueWith(ttt =>
                {
                    Interlocked.Decrement(ref counter);
                    Interlocked.Increment(ref processingCounter);
                    // var obj = JsonConvert.DeserializeObject<ExpandoObject>(ttt.Result) as IDictionary<string, object>;
                    var val = ttt.Result;//(string)obj["value"];
                    return PrepareRequest(
                        new Uri(
                            $"http://localhost:9000/listener/TransactionRegistry({val})/AMSLLC.Listener.Process()"),
                        null).ContinueWith(tttt => Interlocked.Decrement(ref processingCounter));
                }, TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap();


                tasks.Add(t3);
            }

            //Parallel.ForEach(tasks,
            //    task => ;

            var tm = new Timer(new TimerCallback(o => Log.Logger.Information("Parallel count: Open: {0}, Process: {1}", counter, processingCounter)), null, 0, 100);
            await Task.WhenAll(tasks);
            tm.Dispose();
        }

        /// <summary>
        /// Opens the transaction.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        private Task<string> OpenTransaction(Uri uri, BaseListenerRequestMessage request)
        {
            var d = request;
            ListenerClient client = new ListenerClient(uri);



            var response = client.OpenTransactionAsync(request);
            return response.ContinueWith(t =>
            {
                client.Dispose();
                if (t.IsFaulted)
                {
                    t.Exception?.Handle(x =>
                    {
                        Log.Logger.Error("An error has occured while failing transaction {0}", JsonConvert.SerializeObject(t.Exception));
                        return true;
                    });
                }
                else
                {
                    Log.Information("Opened transaction at {0}", uri);
                }

                return t.Result;
            });
        }

        private Task<string> PrepareRequest(Uri uri, object data)
        {
            var d = data;
            HttpClient client = new HttpClient();

            Task<HttpResponseMessage> response;

            client.DefaultRequestHeaders.Add("AMS-Company", AppConfig.CompanyCode);
            client.DefaultRequestHeaders.Add("AMS-Application", AppConfig.ApplicationCode);
            client.DefaultRequestHeaders.Add("AMS-CompanyId", AppConfig.CompanyId);

            if (data != null)
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                response = client.PostAsync(uri,
                            new StringContent(JsonConvert.SerializeObject(d), Encoding.UTF8, "application/json"));
            }
            else
            {
                response = client.PostAsync(uri, new StringContent(string.Empty, Encoding.UTF8));
            }

            return response.ContinueWith(t =>
            {
                client.Dispose();
                Log.Logger.Information("Received from {0}: {1}", uri, t.Result.StatusCode);
                return t.Result.Content.ReadAsStringAsync();
            }).Unwrap();

        }
    }
}
