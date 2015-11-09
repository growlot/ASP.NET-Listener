// <copyright file="OwnerSite.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Site information relevant to ensure unique fields in one owner context.
    /// </summary>
    internal class OwnerSite : Entity<int>
    {
        private IList<IDomainEvent> events;

        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerSite"/> class.
        /// </summary>
        /// <param name="events">The events.</param>
        public OwnerSite(IList<IDomainEvent> events)
        {
            this.events = events;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the premise number.
        /// Usually premisse number is generated and assigned to site by billing system (CIS, CSS, etc.).
        /// If it is set, it must be unique, but if site is not assigned to any billing account then premise number might be empty.
        /// </summary>
        /// <value>
        /// The premise number.
        /// </value>
        public string PremiseNumber { get; private set; }

        /// <summary>
        /// Updates the site details.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="newPremiseNumber">The new premise number.</param>
        /// <param name="newDescription">The new description.</param>
        /// <exception cref="System.ArgumentNullException">Site can not be updated, because site description is required field.</exception>
        public void UpdateSiteDetails(int owner, string newPremiseNumber, string newDescription)
        {
            if (newDescription == null)
            {
                throw new ArgumentNullException(nameof(newDescription), "Site can not be updated, because site description is required field.");
            }

            this.Description = newDescription;
            this.PremiseNumber = newPremiseNumber;

            this.events.Add(
                new SiteDetailsUpdated(
                    owner,
                    this.Id,
                    this.Description,
                    this.PremiseNumber));
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var ownerSiteMemento = (OwnerSiteMemento)memento;
            this.Id = ownerSiteMemento.Id;
            this.Description = ownerSiteMemento.Description;
            this.PremiseNumber = ownerSiteMemento.PremiseNumber;
        }
    }
}
