// //-----------------------------------------------------------------------
// // <copyright file="IApplicationServiceScope.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace WNP.Listener.ApplicationService
{
    using System;
    using AMSLLC.Listener.Domain;
    using Repository;

    public interface IApplicationServiceScope : IDisposable
    {
        IDomainBuilder DomainBuilder { get; }
        IRepositoryBuilder RepositoryBuilder { get; }
    }
}