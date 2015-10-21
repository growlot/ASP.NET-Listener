// //-----------------------------------------------------------------------
// <copyright file="IWithDomainBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Interface to provide domain builder within domain model
    /// </summary>
    public interface IWithDomainBuilder
    {
        /// <summary>
        /// Gets or sets the domain builder.
        /// </summary>
        /// <value>The domain builder.</value>
        IDomainBuilder DomainBuilder { get; set; }
    }
}