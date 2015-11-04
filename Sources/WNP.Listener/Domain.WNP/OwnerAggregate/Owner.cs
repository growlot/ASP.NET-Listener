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
        private IList<OwnerSite> sites = new List<OwnerSite>();

        /// <summary>
        /// Adds the site for this owner.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="address">The address.</param>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <returns>
        /// The new site.
        /// </returns>
        public Site AddSite(BillingAccount account, PhysicalAddress address, string description, string premiseNumber)
        {
            if (description == null)
            {
                throw new ArgumentNullException(nameof(description), "Site can not be created, because site description is required field.");
            }

            foreach (var site in this.sites)
            {
                if (description.Equals(site.Description, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException("Can not add Site, because description is not unique.");
                }

                if (premiseNumber?.Equals(site.PremiseNumber, StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    throw new InvalidOperationException("Can not add Site, because premise number is not unique.");
                }
            }

            var siteBuilder = new SiteBuilder()
                .BilledTo(account)
                .LocatedAt(address)
                .WithDescription(description)
                .WithPremiseNumber(premiseNumber);

            return siteBuilder;
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
                var site = new OwnerSite();
                ((IOriginator)site).SetMemento(siteMemento);
                this.sites.Add(site);
            }
        }
    }
}
