// <copyright file="IListenerProxy.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Client
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for Listener proxy.
    /// </summary>
    public interface IListenerProxy : IDisposable
    {
        /// <summary>
        /// Opens the asynchronous.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        Task<string> OpenAsync(Uri uri, object data);
    }
}