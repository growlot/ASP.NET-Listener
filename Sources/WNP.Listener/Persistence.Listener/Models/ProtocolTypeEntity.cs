// <copyright file="ProtocolTypeEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Persistence.Listener
{
    using System.ComponentModel.DataAnnotations;

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
