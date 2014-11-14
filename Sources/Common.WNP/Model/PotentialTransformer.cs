//-----------------------------------------------------------------------
// <copyright file="PotentialTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing potential transformer device 
    /// </summary>
    public class PotentialTransformer : Transformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialTransformer"/> class.
        /// </summary>
        public PotentialTransformer()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PotentialTransformer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public PotentialTransformer(int id)
        {
            this.Id = id;
        }
    }
}
