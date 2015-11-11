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

    public static class GenericProxy
    {
        public static Task<string> OpenAsync(Uri uri, object data, Dictionary<string, string> headers = null)
        {
            var d = data;
            HttpClient client = new HttpClient();

            Task<HttpResponseMessage> response;

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> keyValuePair in headers)
                {
                    client.DefaultRequestHeaders.Add(keyValuePair.Key, keyValuePair.Value);
                }
                //client.DefaultRequestHeaders.Add("AMS-Company", "CCD");
                //client.DefaultRequestHeaders.Add("AMS-Application", "dde3ff6d-e368-4427-b75e-6ec47183f88e");
            }



            if (d != null)
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
                if (t.Result.StatusCode != HttpStatusCode.OK && t.Result.StatusCode != HttpStatusCode.NoContent)
                {
                    throw new ListenerRequestFailedException(t.Result.StatusCode);
                }
                return t.Result.Content.ReadAsStringAsync();
            }).Unwrap();
        }
    }
}
