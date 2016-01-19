// <copyright file="ValueMapEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ValueMapMetadata))]
    partial class ValueMapEntity
    {
        public virtual ICollection<ValueMapEntryEntity> ValueMapEntries { get; set; } = new Collection<ValueMapEntryEntity>();

        internal sealed class ValueMapMetadata
        {
        }
    }

}
