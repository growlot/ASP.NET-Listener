//-----------------------------------------------------------------------
// <copyright file="SiteBillingAccountUpdated.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAggregate
{
    /// <summary>
    /// The event that updates site billing account information.
    /// </summary>
    public class SiteBillingAccountUpdated : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteBillingAccountUpdated" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="accountName">Name of the account.</param>
        /// <param name="accountNumber">The account number.</param>
        public SiteBillingAccountUpdated(
            int owner,
            int siteId,
            string accountName,
            string accountNumber)
        {
            this.Owner = owner;
            this.SiteId = siteId;
            this.AccountName = accountName;
            this.AccountNumber = accountNumber;
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
        /// Gets the account name.
        /// </summary>
        /// <value>
        /// The account name.
        /// </value>
        public string AccountName { get; private set; }

        /// <summary>
        /// Gets the account number.
        /// </summary>
        /// <value>
        /// The account number.
        /// </value>
        public string AccountNumber { get; private set; }
    }
}
