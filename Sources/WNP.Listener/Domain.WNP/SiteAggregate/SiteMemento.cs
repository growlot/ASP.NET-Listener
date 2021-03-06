﻿//-----------------------------------------------------------------------
// <copyright file="SiteMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System.Collections.Generic;
    using CircuitChild;

    /// <summary>
    /// Memento class for site aggregate root
    /// </summary>
    public class SiteMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMemento" /> class.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="description">The description.</param>
        /// <param name="country">The country.</param>
        /// <param name="state">The state.</param>
        /// <param name="city">The city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="zip">The zip.</param>
        /// <param name="premiseNumber">The premise number.</param>
        /// <param name="billingAccountName">Name of the billing account.</param>
        /// <param name="billingAccountNumber">The billing account number.</param>
        /// <param name="isInterconnect">Is this site interconnect site with other utility.</param>
        /// <param name="interconnectUtilityName">Name of the interconnect utility.</param>
        /// <param name="circuits">The site circuits.</param>
        public SiteMemento(
            int site,
            string description,
            string country,
            string state,
            string city,
            string address1,
            string address2,
            string zip,
            string premiseNumber,
            string billingAccountName,
            string billingAccountNumber,
            bool isInterconnect,
            string interconnectUtilityName,
            IEnumerable<IMemento> circuits)
        {
            this.Id = site;
            this.Description = description;
            this.Country = country;
            this.State = state;
            this.City = city;
            this.Address1 = address1;
            this.Address2 = address2;
            this.Zip = zip;
            this.PremiseNumber = premiseNumber;
            this.BillingAccountName = billingAccountName;
            this.BillingAccountNumber = billingAccountNumber;
            this.IsInterconnect = isInterconnect;
            this.InterconnectUtilityName = interconnectUtilityName;
            this.Circuits = circuits == null ? new List<CircuitMemento>() : circuits;
        }

        /// <summary>
        /// Gets or sets the site circuits mementos.
        /// </summary>
        /// <value>
        /// The site circuits mementos.
        /// </value>
        internal IEnumerable<IMemento> Circuits { get; set; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        internal int Id { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        internal string Description { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        internal string Country { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        internal string State { get; private set; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        internal string City { get; private set; }

        /// <summary>
        /// Gets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        internal string Address1 { get; private set; }

        /// <summary>
        /// Gets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        internal string Address2 { get; private set; }

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        internal string Zip { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        internal string PremiseNumber { get; private set; }

        /// <summary>
        /// Gets the name of the billing account.
        /// </summary>
        /// <value>
        /// The name of the billing account.
        /// </value>
        internal string BillingAccountName { get; private set; }

        /// <summary>
        /// Gets the billing account number.
        /// </summary>
        /// <value>
        /// The billing account number.
        /// </value>
        internal string BillingAccountNumber { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the site is connected to other utility.
        /// </summary>
        /// <value>
        /// The is interconnect.
        /// </value>
        internal bool IsInterconnect { get; private set; }

        /// <summary>
        /// Gets the interconnect utility name.
        /// </summary>
        /// <value>
        /// The interconnect utility name.
        /// </value>
        internal string InterconnectUtilityName { get; private set; }
    }
}
