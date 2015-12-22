// <copyright file="DummyClientAdapter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Client.Test
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;

    public class DummyClientAdapter : IHttpClientAdapter
    {
        private HttpClient tmpClient = new HttpClient();

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public HttpRequestHeaders DefaultRequestHeaders => this.tmpClient.DefaultRequestHeaders;

        public Task<HttpResponseMessage> PostAsync(
            Uri uri,
            StringContent content)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        }

        public Task<HttpResponseMessage> GetAsync(
            Uri uri)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.InternalServerError));
        }
    }
}
