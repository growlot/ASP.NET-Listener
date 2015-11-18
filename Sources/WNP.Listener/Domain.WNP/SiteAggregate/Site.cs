//-----------------------------------------------------------------------
// <copyright file="Site.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CircuitChild;
    using CircuitChild.Equipment;

    /// <summary>
    /// Root aggregate for a Site
    /// </summary>
    public class Site : AggregateRoot<int>
    {
        private string description;

        /// <summary>
        /// The premise number.
        /// Usually premisse number is generated and assigned to site by billing system (CIS, CSS, etc.).
        /// If it is set, it must be unique, but if site is not assigned to any billing account then premise number might be empty.
        /// </summary>
        private string premiseNumber;
        private PhysicalAddress address;
        private BillingAccount account;
        private InterconnectSite interconnectInfo;
        private IList<Circuit> circuits = new List<Circuit>();

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
                siteId: this.Id,
                accountName: this.account.Name,
                accountNumber: this.account.Number);

            this.Events.Add(siteBillingAccountUpdated);
        }

        /// <summary>
        /// Adds new circuit to the site.
        /// </summary>
        /// <param name="circuitDescription">The description.</param>
        /// <param name="meterPoint">The meter point.</param>
        /// <param name="servicePoint">The service point.</param>
        /// <param name="hasBracket">Indicator to determine if circuit has a bracket.</param>
        /// <param name="location">The location.</param>
        /// <param name="service">The service.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="installDate">The install date.</param>
        public void AddCircuit(
            string circuitDescription,
            string meterPoint,
            string servicePoint,
            bool hasBracket,
            GeoLocation location,
            ElectricService service,
            string enclosureType,
            DateTime? installDate)
        {
            if (circuitDescription == null)
            {
                throw new ArgumentNullException(nameof(circuitDescription), "Circuit can not be created, because circuit description is required field.");
            }

            foreach (var existingCircuit in this.circuits)
            {
                existingCircuit.EnsureUniqueness(circuitDescription, meterPoint);
            }

            Circuit.CreateCircuit(
                this.Events,
                this.Id,
                this.GetNextCirciutId(),
                circuitDescription,
                meterPoint,
                servicePoint,
                hasBracket,
                location,
                service,
                enclosureType,
                installDate);
        }

        /// <summary>
        /// Updates the circuit details.
        /// </summary>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <param name="circuitDescription">The circuit description.</param>
        /// <param name="enclosureType">Type of the enclosure.</param>
        /// <param name="hasBracket">The has bracket.</param>
        /// <param name="installDate">The install date.</param>
        /// <param name="meterPoint">The meter point.</param>
        /// <param name="servicePoint">The service point.</param>
        public void UpdateCircuitDetails(int circuitId, string circuitDescription, string enclosureType, bool hasBracket, DateTime? installDate, string meterPoint, string servicePoint)
        {
            var circuit = this.circuits.First(c => c.Id == circuitId);

            foreach (var existingCircuit in this.circuits)
            {
                if (existingCircuit.Id != circuitId)
                {
                    existingCircuit.EnsureUniqueness(circuitDescription, meterPoint);
                }
            }

            circuit.UpdateDetails(
                siteId: this.Id,
                newDescription: circuitDescription,
                newEnclosureType: enclosureType,
                newHasBracket: hasBracket,
                newInstallDate: installDate,
                newMeterPoint: meterPoint,
                newServicePoint: servicePoint);
        }

        /// <summary>
        /// Installs the meter in circuit.
        /// </summary>
        /// <param name="circuitId">The circuit identifier.</param>
        /// <param name="meter">The meter.</param>
        /// <param name="installDate">The install date.</param>
        /// <param name="installUser">The install user.</param>
        /// <param name="installServiceOrder">The service order information.</param>
        public void InstallMeter(
            int circuitId,
            CircuitMeter meter,
            DateTime installDate,
            string installUser,
            ServiceOrder installServiceOrder)
        {
            var circuit = this.circuits.First(item => item.Id == circuitId);
            circuit.InstallMeter(this.Id, meter, installDate, installUser, installServiceOrder);
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        protected override void SetMemento(IMemento memento)
        {
            var siteMemento = (SiteMemento)memento;
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

            foreach (var circuitMemento in siteMemento.Circuits)
            {
                var circuit = new Circuit(this.Events);
                ((IOriginator)circuit).SetMemento(circuitMemento);
                this.circuits.Add(circuit);
            }
        }

        /// <summary>
        /// Gets the identifier for new circiut.
        /// </summary>
        /// <returns>The circuit identifier.</returns>
        private int GetNextCirciutId()
        {
            if (this.circuits.Count == 0)
            {
                return 0;
            }

            return this.circuits.Max(item => item.Id) + 1;
        }
    }
}
