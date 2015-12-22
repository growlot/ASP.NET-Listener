// <copyright file="ListenerProxy.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

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
        private IHttpClientAdapter httpClient = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerProxy"/> class.
        /// </summary>
        /// <param name="headers">The headers.</param>
        public ListenerProxy(Dictionary<string, string> headers)
            : this(new HttpClientAdapter(), headers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerProxy"/> class.
        /// </summary>
        /// <param name="adapter">The adapter.</param>
        /// <param name="headers">The headers.</param>
        public ListenerProxy(IHttpClientAdapter adapter, Dictionary<string, string> headers)
        {
            this.httpClient = adapter;

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in headers)
                {
                    this.httpClient.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ListenerProxy"/> class.
        /// </summary>
        ~ListenerProxy()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        public Task<string> OpenAsync(Uri uri, object data)
        {
            Task<HttpResponseMessage> response;

            if (data != null)
            {
                if (data is string)
                {
                    response = this.httpClient.PostAsync(
                        uri,
                        new StringContent($"={data}", Encoding.UTF8, "application/x-www-form-urlencoded"));
                }
                else
                {
                    this.httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    response = this.httpClient.PostAsync(
                        uri,
                        new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json"));
                }
            }
            else
            {
                response = this.httpClient.PostAsync(uri, new StringContent(string.Empty, Encoding.UTF8));
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.httpClient.Dispose();
            }
        }
    }
}
