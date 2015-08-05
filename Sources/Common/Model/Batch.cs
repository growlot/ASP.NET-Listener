//-----------------------------------------------------------------------
// <copyright file="Batch.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data model class representing Batch
    /// </summary>
    public class Batch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        public Batch()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Batch"/> class.
        /// </summary>
        /// <param name="id">The batch identifier.</param>
        public Batch(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the batch identifier.
        /// </summary>
        /// <value>
        /// The batch identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the total number of chunks in this batch.
        /// </summary>
        /// <value>
        /// The total chunks.
        /// </value>
        public int TotalChunks { get; set; }

        /// <summary>
        /// Gets or sets the latest successfully processed chunk.
        /// </summary>
        /// <value>
        /// The latest chunk.
        /// </value>
        public int LatestChunk { get; set; }
    }
}
