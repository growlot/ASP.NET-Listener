//-----------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Interface for managing unit of work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Commits this unit of work.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this unit of work.
        /// </summary>
        void Rollback();
    }
}
