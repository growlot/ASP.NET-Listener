// <copyright file="CurrentRequestScope.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService.Implementations
{
    using Domain;

    /// <summary>
    /// Implements storing of current request information.
    /// </summary>
    public class CurrentRequestScope : ICurrentRequestScope
    {
        /// <inheritdoc/>
        public int OperatingCompany { get; set; }

        /// <inheritdoc/>
        public IUnitOfWork UnitOfWork { get; set; }

        /// <inheritdoc/>
        public string User { get; set; }
    }
}
