// <copyright file="ICurrentRequestScope.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using Domain;

    /// <summary>
    /// Interface for objects needed in current request scope.
    /// </summary>
    public interface ICurrentRequestScope
    {
        /// <summary>
        /// Gets the WNP user who is performing this request.
        /// </summary>
        /// <value>
        /// The WNP user name.
        /// </value>
        string User { get; }

        /// <summary>
        /// Gets the operating company (owner) of current request.
        /// </summary>
        /// <value>
        /// The operating company.
        /// </value>
        int OperatingCompany { get; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        IUnitOfWork UnitOfWork { get; }
    }
}
