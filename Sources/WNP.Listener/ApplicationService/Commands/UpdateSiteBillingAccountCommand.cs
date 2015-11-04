// <copyright file="UpdateSiteBillingAccountCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Commands
{
    /// <summary>
    /// Information needed for sites billing account update.
    /// </summary>
    public class UpdateSiteBillingAccountCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; set; }

        /// <summary>
        /// Gets or sets the site identifier.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public int SiteId { get; set; }

        /// <summary>
        /// Gets or sets the name of the billing account.
        /// </summary>
        /// <value>
        /// The name of the billing account.
        /// </value>
        public string BillingAccountName { get; set; }

        /// <summary>
        /// Gets or sets the billing account number.
        /// </summary>
        /// <value>
        /// The billing account number.
        /// </value>
        public string BillingAccountNumber { get; set; }
    }
}
