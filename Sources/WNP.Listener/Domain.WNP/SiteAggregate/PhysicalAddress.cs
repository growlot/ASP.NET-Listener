//-----------------------------------------------------------------------
// <copyright file="PhysicalAddress.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    using System;
    using System.Text;

    /// <summary>
    /// Physical address object value type
    /// </summary>
    /// <remarks>
    /// At least first address line needs to be specified for address to be valid.
    /// Address can be converted to string. Converstion is based on the country code, and will pvoride different address format that is specific to this country.
    /// </remarks>
    public sealed class PhysicalAddress : ValueObject<PhysicalAddress>
    {
        /// <summary>
        /// The country
        /// </summary>
        private readonly string country;

        /// <summary>
        /// The state
        /// </summary>
        private readonly string state;

        /// <summary>
        /// The city
        /// </summary>
        private readonly string city;

        /// <summary>
        /// The address1
        /// </summary>
        private readonly string address1;

        /// <summary>
        /// The address2
        /// </summary>
        private readonly string address2;

        /// <summary>
        /// The zip
        /// </summary>
        private readonly string zip;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhysicalAddress"/> class.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <param name="state">The state.</param>
        /// <param name="city">The city.</param>
        /// <param name="address1">The address1.</param>
        /// <param name="address2">The address2.</param>
        /// <param name="zip">The zip.</param>
        public PhysicalAddress(string country, string state, string city, string address1, string address2, string zip)
        {
            if (string.IsNullOrWhiteSpace(address1))
            {
                throw new ArgumentException("Address must have at least first address line specified.", nameof(address1));
            }

            this.country = country;
            this.state = state;
            this.city = city;
            this.address1 = address1;
            this.address2 = address2;
            this.zip = zip;
        }

        /// <summary>
        /// Gets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country
        {
            get
            {
                return this.country;
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State
        {
            get
            {
                return this.state;
            }
        }

        /// <summary>
        /// Gets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City
        {
            get
            {
                return this.city;
            }
        }

        /// <summary>
        /// Gets the address (first line).
        /// </summary>
        /// <value>
        /// The address (first line).
        /// </value>
        public string Address1
        {
            get
            {
                return this.address1;
            }
        }

        /// <summary>
        /// Gets the address (second line).
        /// </summary>
        /// <value>
        /// The address (second line).
        /// </value>
        public string Address2
        {
            get
            {
                return this.address2;
            }
        }

        /// <summary>
        /// Gets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string Zip
        {
            get
            {
                return this.zip;
            }
        }

        /// <summary>
        /// Gets the address in US recommended format.
        /// Only the United States Postal Service (USPS) can deliver to a P.O. Box.
        /// For this reason the recipient may choose to insert their physical (aka street) address as line two,
        /// expanding the complete address to four lines.
        /// Providing both allows a sender to ship via the USPS or via a private carrier.
        /// (<see href="https://en.wikipedia.org/wiki/Address_(geography)#Address_format">source</see>)
        /// </summary>
        /// <returns>Physical address as a string.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "This is a method, not a property.")]
        public string GetAddressUSFormat()
        {
            // If second address line is specified then it should be street address and first line (P.O. Box address)
            // should not be included in string representation
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(this.Address2))
            {
                builder.Append(this.Address2);
                builder.Append(", ");
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.Address1))
                {
                    builder.Append(this.Address1);
                    builder.Append(", ");
                }
            }

            if (!string.IsNullOrWhiteSpace(this.City))
            {
                builder.Append(this.City);
                builder.Append(", ");
            }

            if (!string.IsNullOrWhiteSpace(this.State))
            {
                builder.Append(this.State);
                builder.Append(" ");
            }

            if (!string.IsNullOrWhiteSpace(this.Zip))
            {
                builder.Append(this.Zip);
                builder.Append(" ");
            }

            return builder.ToString().Trim();
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="string" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            // address is formated based on country code
            // if no country specific format is available, then US address format is used.
            switch (this.Country)
            {
                case "US":
                    return this.GetAddressUSFormat();
                default:
                    return this.GetAddressUSFormat();
            }
        }
    }
}
