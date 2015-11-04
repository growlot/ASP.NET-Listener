// <copyright file="OwnerSite.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    /// <summary>
    /// Site information relevant to ensure unique fields in one owner context.
    /// </summary>
    internal class OwnerSite : Entity<int>
    {
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
