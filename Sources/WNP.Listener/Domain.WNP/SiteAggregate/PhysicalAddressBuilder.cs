//-----------------------------------------------------------------------
// <copyright file="PhysicalAddressBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    /// <summary>
    /// Provides fluent interface for building physical address object
    /// </summary>
    public class PhysicalAddressBuilder
    {
        /// <summary>
        /// The country
        /// </summary>
        private string country;

        /// <summary>
        /// The state
        /// </summary>
        private string state;

        /// <summary>
        /// The city
        /// </summary>
        private string city;

        /// <summary>
        /// The address1
        /// </summary>
        private string address1;

        /// <summary>
        /// The address2
        /// </summary>
        private string address2;

        /// <summary>
        /// The zip
        /// </summary>
        private string zip;

        /// <summary>
        /// Performs an implicit conversion from <see cref="PhysicalAddressBuilder"/> to <see cref="PhysicalAddress"/>.
        /// </summary>
        /// <param name="addressBuilder">The address builder.</param>
        /// <returns>
        /// The physical address.
        /// </returns>
        public static implicit operator PhysicalAddress(PhysicalAddressBuilder addressBuilder)
        {
            return ToPhysicalAddress(addressBuilder);
        }

        /// <summary>
        /// Performs a conversion from <see cref="PhysicalAddressBuilder"/> to <see cref="PhysicalAddress"/>.
        /// </summary>
        /// <param name="addressBuilder">The address builder.</param>
        /// <returns>
        /// The physical address.
        /// </returns>
        public static PhysicalAddress ToPhysicalAddress(PhysicalAddressBuilder addressBuilder)
        {
            if (addressBuilder != null)
            {
                return new PhysicalAddress(
                    country: addressBuilder.country,
                    state: addressBuilder.state,
                    city: addressBuilder.city,
                    address1: addressBuilder.address1,
                    address2: addressBuilder.address2,
                    zip: addressBuilder.zip);
            }

            return null;
        }

        /// <summary>
        /// Creates the physical address.
        /// </summary>
        /// <param name="address1">The first address line.</param>
        /// <returns>
        /// The physical address bulder object
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "address1", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder CreatePhysicalAddress(string address1)
        {
            this.address1 = address1;
            return this;
        }

        /// <summary>
        /// Adds country to address definition.
        /// </summary>
        /// <param name="country">The country.</param>
        /// <returns>The physical address bulder object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "country", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder WithCountry(string country)
        {
            this.country = country;
            return this;
        }

        /// <summary>
        /// Adds state to address definition.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns>The physical address bulder object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "state", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder WithState(string state)
        {
            this.state = state;
            return this;
        }

        /// <summary>
        /// Adds city to address definition.
        /// </summary>
        /// <param name="city">The city.</param>
        /// <returns>The physical address bulder object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "city", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder WithCity(string city)
        {
            this.city = city;
            return this;
        }

        /// <summary>
        /// Adds second address line to address definition.
        /// </summary>
        /// <param name="address2">The second address line.</param>
        /// <returns>The physical address bulder object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "address2", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder WithAddressLine2(string address2)
        {
            this.address2 = address2;
            return this;
        }

        /// <summary>
        /// Adds zip code to address definition.
        /// </summary>
        /// <param name="zip">The zip code.</param>
        /// <returns>The physical address bulder object.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "zip", Justification = "Short method in Builder class.")]
        public PhysicalAddressBuilder WithZipCode(string zip)
        {
            this.zip = zip;
            return this;
        }
    }
}
