// <copyright file="FieldConfiguration.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;

    /// <summary>
    /// Field Configuration
    /// </summary>
    public class FieldConfiguration : Entity<Guid>
    {
        /// <inheritdoc/>
        protected override void SetMemento(
            IMemento memento)
        {
            var myMemento = (FieldConfigurationMemento)memento;
            this.Id = myMemento.Id;
        }
    }
}