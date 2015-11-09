using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Communication
{
    using System.Dynamic;
    using System.Net.Http;
    using System.Threading;
    using Newtonsoft.Json;
    using Serilog;

    public class CommunicationManager
    {
        public int ParallelRequests { get; }

        public CommunicationManager(int parallelRequests)
        {
            ParallelRequests = parallelRequests;
        }

        public async Task Run(Uri uri, object[] data)
        {
            int counter = 0;
            int processingCounter = 0;
            List<Task> tasks = new List<Task>();
            foreach (var o in data)
            {
                var t1 = Task.Run(() => Interlocked.Increment(ref counter)).ContinueWith(t => PrepareRequest(uri, o)).Unwrap();




                var t3 = t1.ContinueWith(ttt =>
                {
                    Interlocked.Decrement(ref counter);
                    Interlocked.Increment(ref processingCounter);
                    var obj = JsonConvert.DeserializeObject<ExpandoObject>(ttt.Result) as IDictionary<string, object>;
                    var val = (string)obj["value"];
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

        private Task<string> PrepareRequest(Uri uri, object data)
        {
            var d = data;
            HttpClient client = new HttpClient();

            Task<HttpResponseMessage> response;

            client.DefaultRequestHeaders.Add("AMS-Company", "CCD");
            client.DefaultRequestHeaders.Add("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e");

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
