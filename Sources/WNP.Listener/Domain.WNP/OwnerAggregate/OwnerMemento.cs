//-----------------------------------------------------------------------
// <copyright file="OwnerMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    using System.Collections.Generic;

    /// <summary>
    /// Memento class for owner aggregate root
    /// </summary>
    public class OwnerMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerMemento"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="sites">The sites.</param>
        public OwnerMemento(
                    int owner,
                    IEnumerable<IMemento> sites)
        {
            this.Owner = owner;
            if (sites != null)
            {
                this.Sites = sites;
            }
            else
            {
                this.Sites = new List<OwnerSiteMemento>();
            }
        }

        /// <summary>
        /// Gets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        internal int Owner { get; private set; }

        /// <summary>
        /// Gets the list of sites that belong to this owner.
        /// </summary>
        /// <value>
        /// The list of sites.
        /// </value>
        internal IEnumerable<IMemento> Sites { get; private set; }
    }
}
