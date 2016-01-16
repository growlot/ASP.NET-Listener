// <copyright file="EnabledOperation.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Operation
{
    using System;

    /// <summary>
    /// Enabled operation entity
    /// </summary>
    public class EnabledOperation : ValueObject<EnabledOperation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnabledOperation"/> class.
        /// </summary>
        /// <param name="operationName">Name of the operation.</param>
        public EnabledOperation(string operationName)
        {
            this.OperationName = operationName;
        }

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        /// <value>The name of the operation.</value>
        public string OperationName { get; private set; }
    }
}