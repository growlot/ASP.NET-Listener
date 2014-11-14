//-----------------------------------------------------------------------
// <copyright file="CurrentTransformerBarcode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing current transformer barcode
    /// </summary>
    public class CurrentTransformerBarcode : TransformerBarcode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentTransformerBarcode"/> class.
        /// </summary>
        public CurrentTransformerBarcode()
        {
            this.BarcodeId = new BarcodeIdentifier();
        }

        /// <summary>
        /// Gets or sets the burden1.
        /// </summary>
        /// <value>
        /// The burden1.
        /// </value>
        public float Burden1 { get; set; }

        /// <summary>
        /// Gets or sets the burden2.
        /// </summary>
        /// <value>
        /// The burden2.
        /// </value>
        public float Burden2 { get; set; }
    }
}