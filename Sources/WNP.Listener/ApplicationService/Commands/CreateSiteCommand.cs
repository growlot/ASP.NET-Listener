//-----------------------------------------------------------------------
// <copyright file="CreateSiteCommand.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    using Domain;
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
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public PhysicalAddress Address { get; set; }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <value>
        /// The account.
        /// </value>
        public BillingAccount Account { get; set; }

        /// <summary>
        /// Gets the premise number.
        /// Usually premisse number is generated and assigned to site by billing system (CIS, CSS, etc.).
        /// If it is set, it must be unique, but if site is not assigned to any billing account then premise number might be empty.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; set; }
    }
}
