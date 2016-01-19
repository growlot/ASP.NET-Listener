// <copyright file="EnabledOperationEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AMSLLC.Listener.Persistence.Listener
{
    /// <summary>
    /// Extends <see cref="EnabledOperationEntity"/> class
    /// </summary>
    [MetadataType(typeof(EnabledOperationEntity.EnabledOperationMetadata))]
    partial class EnabledOperationEntity
    {
        /// <summary>
        /// Gets or sets the operation.
        /// </summary>
        /// <value>
        /// The operation.
        /// </value>
        public virtual OperationEntity Operation { get; set; }

        /// <summary>
        /// The metadata for <see cref="EnabledOperationEntity"/>
        /// </summary>
        internal sealed class EnabledOperationMetadata
        {
            [ForeignKey("Operation")]
            public int OperationId { get; set; }
        }
    }
}
