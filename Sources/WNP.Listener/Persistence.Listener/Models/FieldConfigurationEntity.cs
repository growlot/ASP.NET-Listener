// <copyright file="FieldConfigurationEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Collections.ObjectModel;

    public partial class FieldConfigurationEntity
    {
        public virtual ICollection<FieldConfigurationEntryEntity> FieldConfigurationEntries { get; set; } = new Collection<FieldConfigurationEntryEntity>();
    }
}
