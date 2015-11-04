//-----------------------------------------------------------------------
// <copyright file="UpdateSiteDetails.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService.Commands
{
    /// <summary>
    /// Information needed for site details update.
    /// </summary>
    public class UpdateSiteDetails : ICommand
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
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; set; }

        /// <summary>
        /// Gets or sets the is interconnect.
        /// </summary>
        /// <value>
        /// The is interconnect.
        /// </value>
        public string IsInterconnect { get; set; }

        /// <summary>
        /// Gets or sets the interconnect utility name.
        /// </summary>
        /// <value>
        /// The interconnect utility name.
        /// </value>
        public string InterconnectUtilityName { get; set; }
    }
}
