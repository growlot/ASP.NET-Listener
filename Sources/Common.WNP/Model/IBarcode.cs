//-----------------------------------------------------------------------
// <copyright file="IBarcode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    /// <summary>
    /// Interface representing basic equipment
    /// </summary>
    public interface IBarcode
    {
        /// <summary>
        /// Gets or sets the barcode identifier.
        /// </summary>
        /// <value>
        /// The barcode identifier.
        /// </value>
        BarcodeIdentifier BarcodeId { get; set; }

        /// <summary>
        /// Gets or sets the barcode lookup code.
        /// </summary>
        /// <value>
        /// The barcode lookup code.
        /// </value>
        string LookupCode { get; set; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        Owner Owner { get; set; }
    }
}
