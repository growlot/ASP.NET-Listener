// <copyright file="HttpClientAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class HttpClientAdapter : IHttpClientAdapter
    {
        private HttpClient client = null;

        public HttpClientAdapter()
            : this(new HttpClient())
        {
        }

        public HttpClientAdapter(HttpClient client)
        {
            this.client = client;
        }

        ~HttpClientAdapter()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        public HttpRequestHeaders DefaultRequestHeaders => this.client.DefaultRequestHeaders;

        public Task<HttpResponseMessage> PostAsync(Uri uri, StringContent content)
        {
            return this.client.PostAsync(uri, content);
        }

        public Task<HttpResponseMessage> GetAsync(
            Uri uri)
        {
            return this.client.GetAsync(uri);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.client.Dispose();
            }
        }
    }
}
