//-----------------------------------------------------------------------
// <copyright file="IWNPPersistenceController.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common;

    /// <summary>
    /// Extended interface to access different business systems implementing persistence.
    /// Additionally client specific systems are added.
    /// </summary>
    public interface IWNPPersistenceController : IPersistenceController
    {
        /// <summary>
        /// Gets the WNP system.
        /// </summary>
        /// <value>
        /// The WNP system.
        /// </value>
        WNPSystem WNPSystem { get; }
    }
}
