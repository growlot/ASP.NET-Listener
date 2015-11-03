// <copyright file="EntityOperationLookup.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    /// <summary>
    /// Enabled Operation lookup entry
    /// </summary>
    public class EntityOperationLookup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityOperationLookup" /> class.
        /// </summary>
        /// <param name="companyCode">The company code.</param>
        /// <param name="applicationKey">The application key.</param>
        /// <param name="operationName">Name of the operation.</param>
        /// <param name="entityName">Name of the entity.</param>
        /// <param name="enabledOperationId">The enabled operation identifier.</param>
        public EntityOperationLookup(
            string companyCode,
            string applicationKey,
            string operationName,
            string entityName,
            int enabledOperationId)
        {
            this.CompanyCode = companyCode;
            this.ApplicationKey = applicationKey;
            this.OperationName = operationName;
            this.EntityName = entityName;
            this.EnabledOperationId = enabledOperationId;
        }

        /// <summary>
        /// Gets or sets the name of the entity.
        /// </summary>
        /// <value>The name of the entity.</value>
        public string EntityName { get; }

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