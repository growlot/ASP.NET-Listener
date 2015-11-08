using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client.Communication
{
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
            List<Task> tasks = new List<Task>();
            foreach (var o in data)
            {
                tasks.Add(Task.Run(() => { }).ContinueWith(t =>
                {
                    Interlocked.Increment(ref counter);
                    return PrepareRequest(uri, o).ContinueWith(tt =>
                    {
                        Interlocked.Decrement(ref counter);
                        Log.Logger.Information("Parallel count: {0}", counter);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
                }));
            }
            await Task.WhenAll(tasks);
        }

        private async Task PrepareRequest(Uri uri, object data)
        {
            var d = data;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(
                    new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("AMS-Company", "CCD");
                client.DefaultRequestHeaders.Add("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e");

                var response = await client.PostAsync(uri, new StringContent(JsonConvert.SerializeObject(d), Encoding.UTF8, "application/json"));
                Log.Logger.Information("Received: {0}", response.StatusCode);
            }
        }
    }
}
