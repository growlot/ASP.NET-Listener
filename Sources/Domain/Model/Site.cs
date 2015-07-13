//-----------------------------------------------------------------------
// <copyright file="Site.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.Model
{
    using System;
    using AMSLLC.Listener.Domain.Events;
    using AMSLLC.Listener.Domain.Mementos;

    /// <summary>
    /// Root aggregate for a Site
    /// </summary>
    public sealed class Site : Entity<int>, IOriginator
    {
        /////// <summary>
        /////// Gets or sets the owner.
        /////// </summary>
        /////// <value>
        /////// The owner.
        /////// </value>
        ////private int Owner { get; set; }

        /////// <summary>
        /////// Gets or sets the description.
        /////// </summary>
        /////// <value>
        /////// The description.
        /////// </value>
        ////private string Description { get; set; }

        /////// <summary>
        /////// The address of the site.
        /////// </summary>
        ////private PhysicalAddress address;

        /////// <summary>
        /////// Gets or sets the account.
        /////// </summary>
        /////// <value>
        /////// The account.
        /////// </value>
        ////private Account Account { get; set; }
        
        /////// <summary>
        /////// Gets or sets the premise number.
        /////// </summary>
        /////// <value>
        /////// The premise number.
        /////// </value>
        ////private string PremiseNumber { get; set; }

        /// <summary>
        /// Updates the site address.
        /// </summary>
        /// <param name="newAddress">The new address.</param>
        /// <exception cref="System.ArgumentNullException">address;Site must have an address.</exception>
        /// <exception cref="System.ArgumentException">At least first address line must be specified for a Site.</exception>
        public void UpdateAddress(PhysicalAddress newAddress)
        {
            if (newAddress == null)
            {
                throw new ArgumentNullException("newAddress", "Site must have an address.");
            }

            if (string.IsNullOrEmpty(newAddress.Address1))
            {
                throw new ArgumentException("At least first address line must be specified for a Site.");
            }

            EventsRegister.Raise(new SiteAddressUpdated(this.Id, newAddress));
        }

        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void IOriginator.SetMemento(IMemento memento)
        {
            var siteMemento = (SiteMemento)memento;
            this.Id = siteMemento.Id;
        }
    }
}
