//-----------------------------------------------------------------------
// <copyright file="BaseFileField.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing common settings for all flat file fields
    /// </summary>
    public class BaseFileField
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public virtual int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        /// <value>
        /// The index.
        /// </value>
        public virtual int Index { get; set; }

        /// <summary>
        /// Gets or sets the field data type.
        /// </summary>
        /// <value>
        /// The field data type.
        /// </value>
        public virtual string FieldType { get; set; }

        /// <summary>
        /// Gets or sets the converter.
        /// </summary>
        /// <value>
        /// The converter.
        /// </value>
        public virtual FileConverter Converter { get; set; }

        /// <summary>
        /// Gets or sets the null value.
        /// </summary>
        /// <value>
        /// The null value.
        /// </value>
        public virtual string NullValue { get; set; }

        /// <summary>
        /// Gets or sets the trim chars.
        /// </summary>
        /// <value>
        /// The trim chars.
        /// </value>
        public virtual string TrimChars { get; set; }

        /// <summary>
        /// Gets or sets the trim mode.
        /// </summary>
        /// <value>
        /// The trim mode.
        /// </value>
        public virtual FileTrimMode TrimMode { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }
    }
}