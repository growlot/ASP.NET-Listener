// <copyright file="SiteCreatedEvent.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    /// <summary>
    /// Event generated after new site is added.
    /// </summary>
    public class SiteCreatedEvent : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteCreatedEvent" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
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
        public SiteCreatedEvent(
            int owner,
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
            string interconnectUtilityName)
        {
            this.Owner = owner;
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
        }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; private set; }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; private set; }

        /// <summary>
        /// Gets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; private set; }

        /// <summary>
        /// Gets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; private set; }

        /// <summary>
        /// The zip
        /// </summary>
        public string Zip { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; private set; }

        /// <summary>
        /// Gets the name of the billing account.
        /// </summary>
        /// <value>
        /// The name of the billing account.
        /// </value>
        public string BillingAccountName { get; private set; }

        /// <summary>
        /// Gets the billing account number.
        /// </summary>
        /// <value>
        /// The billing account number.
        /// </value>
        public string BillingAccountNumber { get; private set; }

        /// <summary>
        /// Gets the is interconnect.
        /// </summary>
        /// <value>
        /// The is interconnect.
        /// </value>
        public bool IsInterconnect { get; private set; }

        /// <summary>
        /// Gets the interconnect utility name.
        /// </summary>
        /// <value>
        /// The interconnect utility name.
        /// </value>
        public string InterconnectUtilityName { get; private set; }
    }
}
