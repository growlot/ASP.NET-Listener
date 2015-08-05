//-----------------------------------------------------------------------
// <copyright file="Barcode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Model
{
    using System;

    /// <summary>
    /// Data model class representing fields common for all barcodes
    /// </summary>
    public class Barcode : IBarcode
    {
        /// <summary>
        /// The owner
        /// </summary>
        private Owner owner;

        /// <summary>
        /// The barcode lookup code
        /// </summary>
        private string lookupCode;

        /// <summary>
        /// The barcode identifier
        /// </summary>
        private BarcodeIdentifier barcodeId;

        /// <summary>
        /// Gets or sets the barcode identifier.
        /// </summary>
        /// <value>
        /// The barcode identifier.
        /// </value>
        public BarcodeIdentifier BarcodeId
        {
            get
            {
                return this.barcodeId;
            }

            set
            {
                this.barcodeId = value;
                if (value != null)
                {
                    this.lookupCode = value.LookupCode;
                    this.owner = value.Owner;
                }
                else
                {
                    this.owner = null;
                    this.lookupCode = string.Empty;
                }
            }
        }

        /// <summary>
        /// Gets or sets the barcode lookup code.
        /// </summary>
        /// <value>
        /// The barcode lookup code.
        /// </value>
        public string LookupCode
        {
            get
            {
                return this.lookupCode;
            }

            set
            {
                this.lookupCode = value;
                this.barcodeId.LookupCode = this.lookupCode;
            }
        }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public Owner Owner
        {
            get
            {
                return this.owner;
            }

            set
            {
                this.owner = value;
                this.barcodeId.Owner = this.owner;
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the custom field1.
        /// </summary>
        /// <value>
        /// The custom field1.
        /// </value>
        public string CustomField1 { get; set; }

        /// <summary>
        /// Gets or sets the custom field2.
        /// </summary>
        /// <value>
        /// The custom field2.
        /// </value>
        public string CustomField2 { get; set; }

        /// <summary>
        /// Gets or sets the custom field3.
        /// </summary>
        /// <value>
        /// The custom field3.
        /// </value>
        public string CustomField3 { get; set; }

        /// <summary>
        /// Gets or sets the custom field4.
        /// </summary>
        /// <value>
        /// The custom field4.
        /// </value>
        public string CustomField4 { get; set; }

        /// <summary>
        /// Gets or sets the custom field5.
        /// </summary>
        /// <value>
        /// The custom field5.
        /// </value>
        public string CustomField5 { get; set; }

        /// <summary>
        /// Gets or sets the custom field6.
        /// </summary>
        /// <value>
        /// The custom field6.
        /// </value>
        public string CustomField6 { get; set; }

        /// <summary>
        /// Gets or sets the custom field7.
        /// </summary>
        /// <value>
        /// The custom field7.
        /// </value>
        public string CustomField7 { get; set; }

        /// <summary>
        /// Gets or sets the custom field8.
        /// </summary>
        /// <value>
        /// The custom field8.
        /// </value>
        public string CustomField8 { get; set; }

        /// <summary>
        /// Gets or sets the custom field9.
        /// </summary>
        /// <value>
        /// The custom field9.
        /// </value>
        public string CustomField9 { get; set; }

        /// <summary>
        /// Gets or sets the custom field10.
        /// </summary>
        /// <value>
        /// The custom field10.
        /// </value>
        public string CustomField10 { get; set; }

        /// <summary>
        /// Gets or sets the custom field11.
        /// </summary>
        /// <value>
        /// The custom field11.
        /// </value>
        public string CustomField11 { get; set; }

        /// <summary>
        /// Gets or sets the custom field12.
        /// </summary>
        /// <value>
        /// The custom field12.
        /// </value>
        public string CustomField12 { get; set; }

        /// <summary>
        /// Gets or sets the custom field13.
        /// </summary>
        /// <value>
        /// The custom field13.
        /// </value>
        public string CustomField13 { get; set; }

        /// <summary>
        /// Gets or sets the custom field14.
        /// </summary>
        /// <value>
        /// The custom field14.
        /// </value>
        public string CustomField14 { get; set; }

        /// <summary>
        /// Gets or sets the custom field15.
        /// </summary>
        /// <value>
        /// The custom field15.
        /// </value>
        public string CustomField15 { get; set; }

        /// <summary>
        /// Gets or sets the custom field16.
        /// </summary>
        /// <value>
        /// The custom field16.
        /// </value>
        public string CustomField16 { get; set; }

        /// <summary>
        /// Gets or sets the custom field17.
        /// </summary>
        /// <value>
        /// The custom field17.
        /// </value>
        public string CustomField17 { get; set; }

        /// <summary>
        /// Gets or sets the custom field18.
        /// </summary>
        /// <value>
        /// The custom field18.
        /// </value>
        public string CustomField18 { get; set; }

        /// <summary>
        /// Gets or sets the custom field19.
        /// </summary>
        /// <value>
        /// The custom field19.
        /// </value>
        public string CustomField19 { get; set; }

        /// <summary>
        /// Gets or sets the custom field20.
        /// </summary>
        /// <value>
        /// The custom field20.
        /// </value>
        public string CustomField20 { get; set; }

        /// <summary>
        /// Gets or sets the custom field21.
        /// </summary>
        /// <value>
        /// The custom field21.
        /// </value>
        public string CustomField21 { get; set; }

        /// <summary>
        /// Gets or sets the custom field22.
        /// </summary>
        /// <value>
        /// The custom field22.
        /// </value>
        public string CustomField22 { get; set; }
    }
}