// <copyright file="ITransactionProxy.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System;

    /// <summary>
    /// Transaction proxy interface
    /// </summary>
    public interface ITransactionProxy : IDisposable
    {
        /// <summary>
        /// Commit this transaction
        /// </summary>
        void Commit();
    }
}