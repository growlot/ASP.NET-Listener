// //-----------------------------------------------------------------------
// <copyright file="IDomainValidator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener
{
    /// <summary>
    /// Domain validator interface
    /// </summary>
    public interface IDomainValidator
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IDomainValidator"/> is valid.
        /// </summary>
        /// <value><c>true</c> if valid; otherwise, <c>false</c>.</value>
        bool Valid { get; }
    }
}