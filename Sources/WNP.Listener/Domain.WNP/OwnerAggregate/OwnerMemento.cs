//-----------------------------------------------------------------------
// <copyright file="OwnerMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain.WNP.OwnerAggregate
{
    /// <summary>
    /// Memento class for owner aggregate root
    /// </summary>
    public class OwnerMemento : IMemento
    {
        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public int Owner { get; set; }
    }
}
