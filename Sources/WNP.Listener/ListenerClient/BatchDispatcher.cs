// <copyright file="BatchDispatcher.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ListenerClient
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using BatchBuilder;
    using Newtonsoft.Json;
    using Repository.WNP;

    public class BatchDispatcher
    {
        private readonly IWnpRepository repository;

        public BatchDispatcher(IWnpRepository repository)
        {
            this.repository = repository;
        }

        public Task<HttpResponseMessage> OpenBatch(string batchNumber)
        {
            IBatchBuilder builder = new MeterTestResultBatchBuilder(repository);

            return builder.Create(batchNumber).ContinueWith(
                t =>
                {
                    var client = new HttpClient
                    {
                        BaseAddress = new Uri("http://localhost:9000/")
                    };

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("AMS-Company", "CCD");
                    client.DefaultRequestHeaders.Add("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e");
                    return client.PostAsync("listener/Batch", new StringContent(JsonConvert.SerializeObject(t.Result), Encoding.UTF8, "application/json")).ContinueWith(
                        tt =>
                        {
                            client.Dispose();
                            return tt.Result;
                        });
                }).Unwrap();

        }
    }
}