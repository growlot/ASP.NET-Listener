//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    using System;

    /// <summary>
    /// Interface for managing unit of work.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commits this unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Saves the changes to database, but doesn't commit the unit of work.
        /// </summary>
        /// <remarks>
        /// This is needed to get auto generated database ID's so that they could be used
        /// in other queries in the same unit of work scope.
        /// </remarks>
        void SaveChanges();

        /// <summary>
        /// Rollbacks this unit of work.
        /// </summary>
        void Rollback();
    }
}
