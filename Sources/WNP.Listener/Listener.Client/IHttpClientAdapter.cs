namespace AMSLLC.Listener.Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public interface IHttpClientAdapter : IDisposable
    {
        HttpRequestHeaders DefaultRequestHeaders { get; }
        Task<HttpResponseMessage> PostAsync(Uri uri, StringContent content);

        Task<HttpResponseMessage> GetAsync(Uri uri);
    }
}