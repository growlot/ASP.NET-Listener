// //-----------------------------------------------------------------------
// // <copyright file="IApplicationServiceScope.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System;
    using AMSLLC.Listener.Domain;
    using Repository;

    public interface IApplicationServiceScope : IDisposable
    {
        DateTime ScopeDateTime { get; }
        DateTime Now { get; }
        IDomainBuilder DomainBuilder { get; }
        IRepositoryManager RepositoryBuilder { get; }
    }
}