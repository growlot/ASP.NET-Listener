//-----------------------------------------------------------------------
// <copyright file="DeviceBatch.cs" company="Advanced Metering Services LLC">
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
    /// Data model class representing batch of new devices.
    /// </summary>
    public class DeviceBatch
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceBatch"/> class.
        /// </summary>
        public DeviceBatch()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceBatch"/> class.
        /// </summary>
        /// <param name="id">The device batch identifier.</param>
        public DeviceBatch(int id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets or sets the device batch identifier.
        /// </summary>
        /// <value>
        /// The device batch identifier.
        /// </value>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets the external identifier.
        /// </summary>
        /// <value>
        /// The external identifier.
        /// </value>
        public string ExternalId { get; set; }

        /// <summary>
        /// Gets or sets the batch number.
        /// </summary>
        /// <value>
        /// The batch number.
        /// </value>
        public string BatchNumber { get; set; }
    }
}