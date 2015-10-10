// //-----------------------------------------------------------------------
// // <copyright file="DomainBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using Core;
    using Domain;

    /// <summary>
    /// Domain builder
    /// </summary>
    public class DomainBuilder : IDomainBuilder
    {
        /// <summary>
        /// Creates domain model instance.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <returns>Domain model.</returns>
        public virtual TDomainModel Create<TDomainModel>() where TDomainModel : IOriginator
        {
            return Create<TDomainModel>(null);
        }

        /// <summary>
        /// Creates domain model instance.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="memento">The memento.</param>
        /// <returns>Domain model.</returns>
        public TDomainModel Create<TDomainModel>(IMemento memento) where TDomainModel : IOriginator
        {
            var returnValue = ApplicationIntegration.DependencyResolver.ResolveType<TDomainModel>();

            if (memento != null)
            {
                ((IOriginator)returnValue).SetMemento(memento);
            }

            var withDomainBuilder = returnValue as IWithDomainBuilder;

            if (withDomainBuilder != null)
            {
                withDomainBuilder.DomainBuilder = this;
            }

            return returnValue;
        }
    }
}