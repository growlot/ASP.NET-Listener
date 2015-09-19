// //-----------------------------------------------------------------------
// // <copyright file="DomainBuilder.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

using AMSLLC.Listener.Domain;

namespace AMSLLC.Listener.ApplicationService
{
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
        public TDomainModel Create<TDomainModel>() where TDomainModel : IOriginator, new()
        {
            return Create<TDomainModel>(null);
        }

        /// <summary>
        /// Creates domain model instance.
        /// </summary>
        /// <typeparam name="TDomainModel">The type of the domain model.</typeparam>
        /// <param name="memento">The memento.</param>
        /// <returns>Domain model.</returns>
        public TDomainModel Create<TDomainModel>(IMemento memento) where TDomainModel : IOriginator, new()
        {
            var returnValue = new TDomainModel();

            if (memento != null)
            {
                ((IOriginator) returnValue).SetMemento(memento);
            }

            return returnValue;
        }
    }
}