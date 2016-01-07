using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSLLC.Listener.Persistence.Listener
{
    [MetadataType(typeof(EntityCategoryOperationEntity.EntityCategoryOperationMetadata))]
    partial class EntityCategoryOperationEntity
    {

        public virtual EntityCategoryEntity EntityCategory { get; set; }
        public virtual EnabledOperationEntity EnabledOperation { get; set; }

        public virtual ICollection<OperationEndpointEntity> OperationEndpoints { get; set; } = new Collection<OperationEndpointEntity>();

        internal sealed class EntityCategoryOperationMetadata
        {
            [ForeignKey("EntityCategory")]
            public int EntityCategoryId { get; set; }

            [ForeignKey("EnabledOperation")]
            public int EnabledOperationId { get; set; }
        }
    }
}
