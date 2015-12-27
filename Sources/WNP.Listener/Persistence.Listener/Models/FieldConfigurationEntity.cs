using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.Collections.ObjectModel;

    public partial class FieldConfigurationEntity
    {
        public virtual ICollection<FieldConfigurationEntryEntity> FieldConfigurationEntries { get; set; } = new Collection<FieldConfigurationEntryEntity>();
    }
}
