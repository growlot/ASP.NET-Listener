// <copyright file="IRepository.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Repository
{
    using System;

    /// <summary>
    /// Repository marker interface
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker interface.")]
    public interface IRepository : IDisposable
    {
    }
}
