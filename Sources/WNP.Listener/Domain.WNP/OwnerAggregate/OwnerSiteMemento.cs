//-----------------------------------------------------------------------
// <copyright file="OwnerSiteMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    /// <summary>
    /// Memento class for site aggregate root
    /// </summary>
    public class OwnerSiteMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerSiteMemento" /> class.
        /// </summary>
        /// <param name="site">The site.</param>
        /// <param name="description">The description.</param>
        /// <param name="premiseNumber">The premise number.</param>
        public OwnerSiteMemento(
            int site,
            string description,
            string premiseNumber)
        {
            this.Id = site;
            this.Description = description;
            this.PremiseNumber = premiseNumber;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        internal int Id { get; private set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        internal string Description { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        internal string PremiseNumber { get; private set; }
    }
}
