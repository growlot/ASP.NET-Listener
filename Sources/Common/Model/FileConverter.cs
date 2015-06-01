//-----------------------------------------------------------------------
// <copyright file="FileConverter.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.Model
{
    /// <summary>
    /// Data model class representing FileConverter entity
    /// </summary>
    public class FileConverter
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first argument.
        /// </summary>
        /// <value>
        /// The first argument.
        /// </value>
        public string Argument1 { get; set; }

        /// <summary>
        /// Gets or sets the second argument.
        /// </summary>
        /// <value>
        /// The second argument.
        /// </value>
        public string Argument2 { get; set; }

        /// <summary>
        /// Gets or sets the third argument.
        /// </summary>
        /// <value>
        /// The third argument.
        /// </value>
        public string Argument3 { get; set; }

        /// <summary>
        /// Gets or sets the kind of the file converter.
        /// </summary>
        /// <value>
        /// The kind of the file converter.
        /// </value>
        public FileConverterKind FileConverterKind { get; set; }
    }
}