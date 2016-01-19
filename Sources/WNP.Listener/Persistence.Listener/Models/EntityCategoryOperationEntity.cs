// <copyright file="EntityCategoryOperationEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
