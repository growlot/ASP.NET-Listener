//-----------------------------------------------------------------------
// <copyright file="CurrentTransformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing current transformer device
    /// </summary>
    public class CurrentTransformer : Transformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTransformer"/> class.
        /// </summary>
        public CurrentTransformer()
            : this(-1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTransformer"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public CurrentTransformer(int id)
        {
            this.Id = id;
        }
    }
}
