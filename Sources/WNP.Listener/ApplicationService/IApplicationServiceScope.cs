// //-----------------------------------------------------------------------
// // <copyright file="IApplicationServiceScope.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

using System;
using AMSLLC.Listener.Domain;
using AMSLLC.Listener.Repository;

namespace AMSLLC.Listener.ApplicationService
{
    public interface IApplicationServiceScope : IDisposable
    {
        IDomainBuilder DomainBuilder { get; }
        IRepositoryBuilder RepositoryBuilder { get; }
    }
}