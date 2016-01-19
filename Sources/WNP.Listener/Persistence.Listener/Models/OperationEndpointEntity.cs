// <copyright file="OperationEndpointEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
