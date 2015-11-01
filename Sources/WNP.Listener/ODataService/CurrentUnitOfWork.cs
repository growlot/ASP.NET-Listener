// <copyright file="CurrentUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService
{
    using Repository.WNP;

    /// <summary>
    /// Unit of work for current http request.
    /// </summary>
    public class CurrentUnitOfWork
    {
        /// <summary>
        /// Gets or sets the unit of work.
        /// </summary>
        /// <value>
        /// The unit of work.
        /// </value>
        public IWNPUnitOfWork UnitOfWork { get; set; }
    }
}
