//-----------------------------------------------------------------------
// <copyright file="TransformerBarcode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    /// <summary>
    /// Data model class extending barcode with fields common for all transformers
    /// </summary>
    public class TransformerBarcode : Barcode
    {
        /// <summary>
        /// Gets or sets the taps.
        /// </summary>
        /// <value>
        /// The taps.
        /// </value>
        public int Taps { get; set; }

        /// <summary>
        /// Gets or sets the ratio.
        /// </summary>
        /// <value>
        /// The ratio.
        /// </value>
        public string Ratio { get; set; }

        /// <summary>
        /// Gets or sets the accuracy class1.
        /// </summary>
        /// <value>
        /// The accuracy class1.
        /// </value>
        public decimal AccuracyClass1 { get; set; }

        /// <summary>
        /// Gets or sets the accuracy class2.
        /// </summary>
        /// <value>
        /// The accuracy class2.
        /// </value>
        public decimal AccuracyClass2 { get; set; }
    }
}
