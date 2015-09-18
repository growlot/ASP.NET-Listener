// //-----------------------------------------------------------------------
// // <copyright file="IDomainBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Domain builder interface
    /// </summary>
    public interface IDomainBuilder
    {
        /// <summary>
        /// Creates domain model instance.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <returns>Domain model.</returns>
        TDomainModel Create<TDomainModel>() where TDomainModel : IOriginator, new();

        /// <summary>
        /// Creates domain model instance.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="memento">The memento.</param>
        /// <returns>Domain model.</returns>
        TDomainModel Create<TDomainModel>(IMemento memento) where TDomainModel : IOriginator, new();
    }
}