//-----------------------------------------------------------------------
// <copyright file="CreateSiteCommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using Domain.WNP.SiteAggregate;

    /// <summary>
    /// Information needed for site creation.
    /// </summary>
    public class CreateSiteCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the address1.
        /// </summary>
        /// <value>
        /// The address1.
        /// </value>
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or sets the address2.
        /// </summary>
        /// <value>
        /// The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        /// The zip
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; set; }

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

        /// <summary>
        /// Gets or sets the site identifier. This is a return value and will be set in command handler on successfull completion.
        /// </summary>
        /// <value>
        /// The site identifier.
        /// </value>
        public string ReturnedSiteId { get; set; }
    }
}
