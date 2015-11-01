//-----------------------------------------------------------------------
// <copyright file="Site.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using Core;

    /// <summary>
    /// Root aggregate for a Site
    /// </summary>
    public class Site : AggregateRoot<int>
    {
        /// <summary>
        /// The domain event bus
        /// </summary>
        private IDomainEventBus domainEventBus = ApplicationIntegration.DependencyResolver.ResolveType<IDomainEventBus>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class.
        /// </summary>
        public Site()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <param name="address">The address.</param>
        /// <param name="account">The account.</param>
        private Site(string description, string premiseNumber, PhysicalAddress address, BillingAccount account)
        {
            this.Description = description;
            this.PremiseNumber = premiseNumber;
            this.Account = account;
            this.Address = address;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public PhysicalAddress Address { get; private set; }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public BillingAccount Account { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// Usually premisse number is generated and assigned to site by billing system (CIS, CSS, etc.).
        /// If it is set, it must be unique, but if site is not assigned to any billing account then premise number might be empty.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; private set; }

        /// <summary>
        /// Creates new site.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <param name="address">The address.</param>
        /// <param name="account">The account.</param>
        /// <returns>The new site.</returns>
        public static Site CreateSite(string description, string premiseNumber, PhysicalAddress address, BillingAccount account)
        {
            return new Site(description, premiseNumber, address, account);
        }

        /// <summary>
        /// Updates the site address.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        /// <exception cref="System.ArgumentNullException">address;Site must have an address.</exception>
        /// <exception cref="System.ArgumentException">At least first address line must be specified for a Site.</exception>
        public void UpdateAddress(PhysicalAddress newAddress)
        {
            if (newAddress == null)
            {
                throw new ArgumentNullException("newAddress", "Site must have an address.");
            }

            if (string.IsNullOrEmpty(newAddress.Address1))
            {
                throw new ArgumentException("At least first address line must be specified for a Site.");
            }

            this.domainEventBus.Publish(new SiteAddressUpdated(this.Id, newAddress));
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var siteMemento = (SiteMemento)memento;
            this.Id = siteMemento.Id;
            this.Description = siteMemento.Description;
            this.PremiseNumber = siteMemento.PremiseNumber;
            if (!string.IsNullOrWhiteSpace(siteMemento.Address1))
            {
                this.Address = new PhysicalAddressBuilder()
                    .CreatePhysicalAddress(siteMemento.Address1)
                    .WithAddressLine2(siteMemento.Address2)
                    .WithCity(siteMemento.City)
                    .WithCountry(siteMemento.Country)
                    .WithState(siteMemento.State)
                    .WithZipCode(siteMemento.Zip);
            }

            if (!string.IsNullOrWhiteSpace(siteMemento.BillingAccountNumber))
            {
                this.Account = new BillingAccount(siteMemento.BillingAccountName, siteMemento.BillingAccountNumber);
            }
        }
    }
}
