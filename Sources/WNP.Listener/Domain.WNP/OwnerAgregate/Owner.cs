//-----------------------------------------------------------------------
// <copyright file="Owner.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAgregate
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SiteAgregate;

    /// <summary>
    /// Handling Sites for specific owner
    /// </summary>
    public sealed class Owner : Entity<int>, IAggregateRoot, IOriginator
    {
        private IList<Site> sites = new List<Site>();

        /// <summary>
        /// Adds the site. Application Service layer is responsible to check if site already exists before trying to add it.
        /// </summary>
        /// <param name="siteBuilder">The site builder.</param>
        /// <returns>The task that creates the site.</returns>
        public Task<Site> AddSite(SiteBuilder siteBuilder)
        {
            Site site = siteBuilder;
            this.sites.Add(site);

            return Task.Factory.StartNew(() => (Site)siteBuilder);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
        }
    }
}
