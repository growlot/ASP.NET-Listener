using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    [MetadataType(typeof(EnabledOperationEntity.EnabledOperationMetadata))]
    partial class EnabledOperationEntity
    {

        public virtual OperationEntity Operation { get; set; }

        internal sealed class EnabledOperationMetadata
        {
            [ForeignKey("Operation")]
            public int OperationId { get; set; }
        }
    }
}
