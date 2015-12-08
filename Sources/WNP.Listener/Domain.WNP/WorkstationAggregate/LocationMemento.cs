// <copyright file="LocationMemento.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    /// <summary>
    /// Memento class for location value object
    /// </summary>
    public class LocationMemento : IMemento
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMemento"/> class.
        /// </summary>
        /// <param name="locationName">Name of the location.</param>
        /// <param name="locationType">Type of the location.</param>
        public LocationMemento(string locationName, string locationType)
        {
            this.LocationName = locationName;
            this.LocationType = locationType;
        }

        internal string LocationName { get; private set; }

        internal string LocationType { get; private set; }
    }
}
