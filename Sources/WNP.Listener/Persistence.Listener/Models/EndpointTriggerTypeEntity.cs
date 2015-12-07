using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(EndpointTriggerTypeMetadata))]
    public partial class EndpointTriggerTypeEntity
    {
        internal sealed class EndpointTriggerTypeMetadata
        {
            [Key()]
            public int EndpointTriggerTypeId { get; set; }

        }
    }
}
