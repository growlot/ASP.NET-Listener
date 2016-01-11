// <copyright file="EndpointTriggerTypeEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

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
