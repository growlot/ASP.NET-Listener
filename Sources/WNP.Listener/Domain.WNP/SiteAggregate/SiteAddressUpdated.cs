//-----------------------------------------------------------------------
// <copyright file="SiteAddressUpdated.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    /// <summary>
    /// The event that updates site address.
    /// </summary>
    public class SiteAddressUpdated : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAddressUpdated" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="country">The country.</param>
        /// <param name="state">The state.</param>
        /// <param name="city">The city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="zip">The zip.</param>
        public SiteAddressUpdated(
            int owner,
            int siteId,
            string country,
            string state,
            string city,
            string address1,
            string address2,
            string zip)
        {
            this.Owner = owner;
            this.SiteId = siteId;
            this.Country = country;
            this.State = state;
            this.City = city;
            this.Address1 = address1;
            this.Address2 = address2;
            this.Zip = zip;
        }

        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; private set; }

        /// <summary>
        /// Gets the site identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int SiteId { get; private set; }

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
    }
}
