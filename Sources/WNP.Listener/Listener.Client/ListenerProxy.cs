namespace AMSLLC.Listener.Client
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Exception;
    using Newtonsoft.Json;
    using Serilog;

    public class ListenerProxy : IListenerProxy
    {

        private IHttpClientAdapter _hClient = null;

        public ListenerProxy(Dictionary<string, string> headers) : this(new HttpClientAdapter(),headers) { }

        public ListenerProxy(IHttpClientAdapter adapter, Dictionary<string, string> headers)
        {
            this._hClient = adapter;

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in headers)
                {
                    this._hClient.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                }
                //client.DefaultRequestHeaders.Add("AMS-Company", "CCD");
                //client.DefaultRequestHeaders.Add("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e");
            }
        }

        public Task<string> OpenAsync(Uri uri, object data)
        {
            var d = data;

            Task<HttpResponseMessage> response;

            if (d != null)
            {
                if (d is string)
                {
                    response = this._hClient.PostAsync(
                        uri,
                        new StringContent($"={d}", Encoding.UTF8, "application/x-www-form-urlencoded"));
                }
                else
                {
                    this._hClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    response = this._hClient.PostAsync(
                        uri,
                        new StringContent(JsonConvert.SerializeObject(d), Encoding.UTF8, "application/json"));
                }
            }
            else
            {
                response = this._hClient.PostAsync(uri, new StringContent(string.Empty, Encoding.UTF8));
            }

            return response.ContinueWith(t =>
            {
                Log.Logger.Information("Received from {0}: {1}", uri, t.Result.StatusCode);
                if (t.Result.StatusCode != HttpStatusCode.OK && t.Result.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ListenerRequestFailedException(t.Result.StatusCode);
                }
                return t.Result.Content.ReadAsStringAsync();
            }).Unwrap();
        }

        public Task<string> GetAsync(
            Uri uri)
        {
            IHttpClientAdapter client = this._hClient;
            var response = client.GetAsync(uri);

            return response.ContinueWith(t =>
            {
                
                Log.Logger.Information("Received from {0}: {1}", uri, t.Result.StatusCode);
                if (t.Result.StatusCode != HttpStatusCode.OK && t.Result.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ListenerRequestFailedException(t.Result.StatusCode);
                }
                return t.Result.Content.ReadAsStringAsync();
            }).Unwrap();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ListenerProxy()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }
        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this._hClient.Dispose();
            }
        }

       
    }
}
