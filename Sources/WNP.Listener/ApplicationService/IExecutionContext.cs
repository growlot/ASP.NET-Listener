// //-----------------------------------------------------------------------
// <copyright file="IExecutionContext.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.ApplicationService
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface defining transaction execution context.
    /// </summary>
    public interface IExecutionContext
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IExecutionContext"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        bool Valid { get; }

        /// <summary>
        /// Adds the error message to indicate that transaction execution has failed.
        /// </summary>
        /// <param name="message">The error message.</param>
        void AddError(string message);

        /// <summary>
        /// Adds the error message associated to specific property to indicate that transaction execution has failed.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="associatedProperty">The associated property.</param>
        void AddError(string message, string associatedProperty);

        /// <summary>
        /// Gets all errors reported for this transaction execution context.
        /// </summary>
        /// <returns>The collection of error messages.</returns>
        IReadOnlyCollection<ErrorMessage> GetErrors();
    }
}