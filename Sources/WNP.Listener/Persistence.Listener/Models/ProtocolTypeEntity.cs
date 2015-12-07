using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [MetadataType(typeof(ProtocolTypeMetadata))]
    public partial class ProtocolTypeEntity
    {
        internal sealed class ProtocolTypeMetadata
        {
            [Key()]
            public int ProtocolTypeId { get; set; }

        }
    }
}
