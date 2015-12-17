namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(ValueMapMetadata))]
    partial class ValueMapEntity
    {
        public virtual ICollection<ValueMapEntryEntity> ValueMapEntries { get; set; } = new Collection<ValueMapEntryEntity>();

        internal sealed class ValueMapMetadata
        {
        }
    }

}
