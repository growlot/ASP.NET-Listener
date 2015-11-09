//-----------------------------------------------------------------------
// <copyright file="SiteDetailsUpdated.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    /// <summary>
    /// The event that updates site details.
    /// </summary>
    public class SiteDetailsUpdated : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteDetailsUpdated" /> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="siteId">The site identifier.</param>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        public SiteDetailsUpdated(
            int owner,
            int siteId,
            string description,
            string premiseNumber)
        {
            this.Owner = owner;
            this.SiteId = siteId;
            this.Description = description;
            this.PremiseNumber = premiseNumber;
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
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; private set; }
    }
}
