// <copyright file="IHttpClientAdapter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for HTTP calls to Listener service.
    /// </summary>
    public interface IHttpClientAdapter : IDisposable
    {
        /// <summary>
        /// Gets the default request headers.
        /// </summary>
        /// <value>
        /// The default request headers.
        /// </value>
        HttpRequestHeaders DefaultRequestHeaders { get; }

        /// <summary>
        /// Performs HTTP POST request asynchronously.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="content">The request content.</param>
        /// <returns>The HTTP response messgae.</returns>
        Task<HttpResponseMessage> PostAsync(Uri uri, StringContent content);

        /// <summary>
        /// Performs HTTP GET request asynchronously.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The HTTP response messgae.</returns>
        Task<HttpResponseMessage> GetAsync(Uri uri);
    }
}