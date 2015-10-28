// <copyright file="EnabledOperationLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Enabled Operation lookup entry
    /// </summary>
    public class EnabledOperationLookup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnabledOperationLookup" /> class.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        public EnabledOperationLookup(string companyCode, string applicationKey, string operationName, int enabledOperationId)
        {
            this.CompanyCode = companyCode;
            this.ApplicationKey = applicationKey;
            this.OperationName = operationName;
            this.EnabledOperationId = enabledOperationId;
        }

        /// <summary>
        /// Gets or sets the enabled operation identifier.
        /// </summary>
        /// <value>The enabled operation identifier.</value>
        public int EnabledOperationId { get; }

        /// <summary>
        /// Gets the company code.
        /// </summary>
        /// <value>The company code.</value>
        public string CompanyCode { get; }

        /// <summary>
        /// Gets the application key.
        /// </summary>
        /// <value>The application key.</value>
        public string ApplicationKey { get; }

        /// <summary>
        /// Gets the name of the operation.
        /// </summary>
        /// <value>The name of the operation.</value>
        public string OperationName { get; }
    }
}