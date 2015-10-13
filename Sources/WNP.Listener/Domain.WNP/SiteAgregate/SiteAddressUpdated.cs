//-----------------------------------------------------------------------
// <copyright file="SiteAddressUpdated.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.SiteAgregate
{
    /// <summary>
    /// The event that updates site address.
    /// </summary>
    public class SiteAddressUpdated : IEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SiteAddressUpdated"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="address">The address.</param>
        public SiteAddressUpdated(int id, PhysicalAddress address)
        {
            this.Id = id;
            this.Address = address;
        }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public PhysicalAddress Address { get; private set; }
    }
}
