//-----------------------------------------------------------------------
// <copyright file="Owner.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    using System;
    using System.Collections.Generic;
    using SiteAggregate;

    /// <summary>
    /// Handling Sites for specific owner
    /// </summary>
    public class Owner : Entity<int>, IAggregateRoot
    {
        private IList<Site> sites = new List<Site>();

        /// <summary>
        /// Adds the site. Application Service layer is responsible to check if site already exists before trying to add it.
        /// </summary>
        /// <param name="siteBuilder">The site builder.</param>
        /// <returns>The site.</returns>
        public Site AddSite(SiteBuilder siteBuilder)
        {
            Site site = siteBuilder;
            this.sites.Add(site);

            return (Site)siteBuilder;
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}
