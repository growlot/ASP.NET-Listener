//-----------------------------------------------------------------------
// <copyright file="Transformer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class extending equipment with transformer specific fields
    /// </summary>
    public class Transformer : Equipment
    {
        /// <summary>
        /// Gets or sets the transformer code.
        /// </summary>
        /// <value>
        /// The transformer code.
        /// </value>
        public string TransformerCode { get; set; }
    }
}
