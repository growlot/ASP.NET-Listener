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
    public class Owner : AggregateRoot<int>
    {
        private IList<Site> sites = new List<Site>();

        /// <summary>
        /// Adds the site. Application Service layer is responsible to check if site already exists before trying to add it.
        /// </summary>
        /// <param name="siteBuilder">The site builder.</param>
        public void AddSite(SiteBuilder siteBuilder)
        {
            Site newSite = siteBuilder;

            foreach (var site in this.sites)
            {
                if (newSite.Description.Equals(site.Description, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Can not add Site, because description is not unique.");
                }

                if (newSite.PremiseNumber?.Equals(site.PremiseNumber, StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    throw new InvalidOperationException("Can not add Site, because premise number is not unique.");
                }
            }

            this.sites.Add(newSite);
            this.Events.Add(
                new SiteCreatedEvent(
                    owner: this.Id,
                    description: newSite.Description,
                    country: newSite.Address?.Country,
                    state: newSite.Address?.State,
                    city: newSite.Address?.City,
                    address1: newSite.Address?.Address1,
                    address2: newSite.Address?.Address2,
                    zip: newSite.Address?.Zip,
                    premiseNumber: newSite.PremiseNumber,
                    billingAccountName: newSite.Account?.Name,
                    billingAccountNumber: newSite.Account?.Number));
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var ownerMemento = (OwnerMemento)memento;
            this.Id = ownerMemento.Owner;
            foreach (IMemento siteMemento in ownerMemento.Sites)
            {
                var site = new Site();
                ((IOriginator)site).SetMemento(siteMemento);
                this.sites.Add(site);
            }
        }
    }
}
