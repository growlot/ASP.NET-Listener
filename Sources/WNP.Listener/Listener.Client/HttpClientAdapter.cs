using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Client
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;

    public class HttpClientAdapter : IHttpClientAdapter
    {
        private HttpClient _client = null;
        public HttpClientAdapter() : this(new HttpClient())
        {
        }

        public HttpClientAdapter(HttpClient client)
        {
            this._client = client;
        }

        public HttpRequestHeaders DefaultRequestHeaders => this._client.DefaultRequestHeaders;

        public Task<HttpResponseMessage> PostAsync(Uri uri, StringContent content)
        {
            return this._client.PostAsync(uri, content);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HttpClientAdapter()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._client.Dispose();
            }
        }
    }
}
