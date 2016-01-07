using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    [MetadataType(typeof(OperationEndpointMetadata))]
    public partial class OperationEndpointEntity
    {
        public EndpointEntity Endpoint { get; set; }

        internal sealed class OperationEndpointMetadata
        {
            [ForeignKey("Endpoint")]
            public int EndpointId { get; set; }
        }
    }
}
