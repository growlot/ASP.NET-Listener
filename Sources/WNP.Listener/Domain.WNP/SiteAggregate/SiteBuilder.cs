//-----------------------------------------------------------------------
// <copyright file="SiteBuilder.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    /// <summary>
    /// Provides fluent interface for building site object
    /// </summary>
    public class SiteBuilder
    {
        private int owner;
        private string description;
        private string premiseNumber;
        private BillingAccount account;
        private PhysicalAddress address;

        /// <summary>
        /// Performs an implicit conversion from <see cref="SiteBuilder"/> to <see cref="Site"/>.
        /// </summary>
        /// <param name="siteBuilder">The site builder.</param>
        /// <returns>
        /// The site.
        /// </returns>
        public static implicit operator Site(SiteBuilder siteBuilder)
        {
            return ToSite(siteBuilder);
        }

        /// <summary>
        /// Performs a conversion from <see cref="SiteBuilder"/> to <see cref="Site"/>.
        /// </summary>
        /// <param name="siteBuilder">The site builder.</param>
        /// <returns>
        /// The site.
        /// </returns>
        public static Site ToSite(SiteBuilder siteBuilder)
        {
            if (siteBuilder != null)
            {
                return Site.CreateSite(
                    siteBuilder.owner,
                    siteBuilder.description,
                    siteBuilder.premiseNumber,
                    siteBuilder.address,
                    siteBuilder.account);
            }

            return null;
        }

        /// <summary>
        /// Creates the site builder.
        /// </summary>
        /// <returns>The site builder object.</returns>
        public SiteBuilder CreateSite()
        {
            return this;
        }

        /// <summary>
        /// Adds description to site definition.
        /// </summary>
        /// <param name="siteDescription">The site description.</param>
        /// <returns>The site builder object</returns>
        public SiteBuilder WithDescription(string siteDescription)
        {
            this.description = siteDescription;
            return this;
        }

        /// <summary>
        /// Adds premise number to site definition.
        /// </summary>
        /// <param name="sitePremiseNumber">The site premise number.</param>
        /// <returns>The site builder object</returns>
        public SiteBuilder WithPremiseNumber(string sitePremiseNumber)
        {
            this.premiseNumber = sitePremiseNumber;
            return this;
        }

        /// <summary>
        /// Adds billing account to site definition.
        /// </summary>
        /// <param name="billingAccount">The billing account.</param>
        /// <returns>The site builder object</returns>
        public SiteBuilder BilledTo(BillingAccount billingAccount)
        {
            this.account = billingAccount;
            return this;
        }

        /// <summary>
        ///  Adds address to site definition.
        /// </summary>
        /// <param name="siteAddress">The site address.</param>
        /// <returns>The site builder object</returns>
        public SiteBuilder LocatedAt(PhysicalAddress siteAddress)
        {
            this.address = siteAddress;
            return this;
        }

        /// <summary>
        ///  Adds address to site definition.
        /// </summary>
        /// <param name="siteOwner">The site owner.</param>
        /// <returns>The site builder object</returns>
        public SiteBuilder OwnedBy(int siteOwner)
        {
            this.owner = siteOwner;
            return this;
        }
    }
}
