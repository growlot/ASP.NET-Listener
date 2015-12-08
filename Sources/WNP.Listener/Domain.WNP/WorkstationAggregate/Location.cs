// <copyright file="Location.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.WorkstationAggregate
{
    using System;

    /// <summary>
    /// Specifies location that can be used to hold equipment
    /// </summary>
    public class Location : Entity<string>
    {
        private string locationType;

        /// <summary>
        /// Checks if this location belongses to specified location type.
        /// </summary>
        /// <param name="locationType">Type of the location.</param>
        /// <returns><c>true</c> if locaiton belongs to specified location type; <c>false</c> otherwise.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "locationType", Justification = "Method is very short, just a check. Doesn't make sense to name parameter some other way.")]
        public bool BelongsTo(string locationType)
        {
            return this.locationType == locationType;
        }

        /// <inheritdoc/>
        protected override void SetMemento(IMemento memento)
        {
            var locationMemento = (LocationMemento)memento;
            this.Id = locationMemento.LocationName;
            this.locationType = locationMemento.LocationType;
        }
    }
}
