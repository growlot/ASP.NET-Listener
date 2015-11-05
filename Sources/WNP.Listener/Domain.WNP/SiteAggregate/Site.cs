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
        /// The owner.
        /// </summary>
        private int owner;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The premise number.
        /// Usually premisse number is generated and assigned to site by billing system (CIS, CSS, etc.).
        /// If it is set, it must be unique, but if site is not assigned to any billing account then premise number might be empty.
        /// </summary>
        private string premiseNumber;

        /// <summary>
        /// The address.
        /// </summary>
        private PhysicalAddress address;

        /// <summary>
        /// The account.
        /// </summary>
        private BillingAccount account;

        private InterconnectSite interconnectInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="Site"/> class.
        /// </summary>
        public Site()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Site" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <param name="address">The address.</param>
        /// <param name="account">The account.</param>
        /// <param name="interconnectInfo">The site interconnect information.</param>
        private Site(
            int owner,
            string description,
            string premiseNumber,
            PhysicalAddress address,
            BillingAccount account,
            InterconnectSite interconnectInfo)
        {
            this.description = description;
            this.premiseNumber = premiseNumber;
            this.account = account;
            this.address = address;
            this.interconnectInfo = interconnectInfo;

            this.Events.Add(
                new SiteCreatedEvent(
                    owner: owner,
                    description: this.description,
                    country: this.address?.Country,
                    state: this.address?.State,
                    city: this.address?.City,
                    address1: this.address?.Address1,
                    address2: this.address?.Address2,
                    zip: this.address?.Zip,
                    premiseNumber: this.premiseNumber,
                    billingAccountName: this.account?.Name,
                    billingAccountNumber: this.account?.Number,
                    isInterconnect: this.interconnectInfo.IsInterconnect,
                    interconnectUtilityName: this.interconnectInfo.Name));
        }

        /// <summary>
        /// Creates new site.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <param name="address">The address.</param>
        /// <param name="account">The account.</param>
        /// <param name="interconnectInfo">The site interconnect information.</param>
        /// <returns>
        /// The new site.
        /// </returns>
        public static Site CreateSite(
            int owner,
            string description,
            string premiseNumber,
            PhysicalAddress address,
            BillingAccount account,
            InterconnectSite interconnectInfo)
        {
            return new Site(owner, description, premiseNumber, address, account, interconnectInfo);
        }

        /// <summary>
        /// Updates the site address.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        /// <exception cref="System.ArgumentNullException">newAddress;New Site address is not specified. Please use ClearAddress action to remove address information from site.</exception>
        public void UpdateAddress(PhysicalAddress newAddress)
        {
            if (newAddress == null)
            {
                throw new ArgumentNullException("newAddress", "New site address is not specified. Please use ClearAddress action to remove address information from site.");
            }

            if (newAddress == this.address)
            {
                return;
            }

            this.address = newAddress;
            var siteAddressUpdated = new SiteAddressUpdated(
                owner: this.owner,
                siteId: this.Id,
                country: this.address.Country,
                state: this.address.State,
                city: this.address.City,
                address1: this.address.Address1,
                address2: this.address.Address2,
                zip: this.address.Zip);

            this.Events.Add(siteAddressUpdated);
        }

        /// <summary>
        /// Updates the billing account.
        /// </summary>
        /// <param name="newAccount">The new account.</param>
        /// <exception cref="System.ArgumentNullException">New billing account for the site is not specified. Please use ClearAccount action to remove account information from site.</exception>
        public void UpdateBillingAccount(BillingAccount newAccount)
        {
            if (newAccount == null)
            {
                throw new ArgumentNullException(nameof(newAccount), "New billing account for the site is not specified. Please use ClearAccount action to remove account information from site.");
            }

            if (newAccount == this.account)
            {
                return;
            }

            this.account = newAccount;
            var siteBillingAccountUpdated = new SiteBillingAccountUpdated(
                owner: this.owner,
                siteId: this.Id,
                accountName: this.account.Name,
                accountNumber: this.account.Number);

            this.Events.Add(siteBillingAccountUpdated);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var siteMemento = (SiteMemento)memento;
            this.owner = siteMemento.Owner;
            this.Id = siteMemento.Id;
            this.description = siteMemento.Description;
            this.premiseNumber = siteMemento.PremiseNumber;
            this.interconnectInfo = new InterconnectSite(siteMemento.IsInterconnect, siteMemento.InterconnectUtilityName);
            if (!string.IsNullOrWhiteSpace(siteMemento.Address1))
            {
                this.address = new PhysicalAddressBuilder()
                    .CreatePhysicalAddress(siteMemento.Address1)
                    .WithAddressLine2(siteMemento.Address2)
                    .WithCity(siteMemento.City)
                    .WithCountry(siteMemento.Country)
                    .WithState(siteMemento.State)
                    .WithZipCode(siteMemento.Zip);
            }

            if (!string.IsNullOrWhiteSpace(siteMemento.BillingAccountNumber))
            {
                this.account = new BillingAccount(siteMemento.BillingAccountName, siteMemento.BillingAccountNumber);
            }
        }
    }
}
