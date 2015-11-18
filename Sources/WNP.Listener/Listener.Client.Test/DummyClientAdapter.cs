using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
