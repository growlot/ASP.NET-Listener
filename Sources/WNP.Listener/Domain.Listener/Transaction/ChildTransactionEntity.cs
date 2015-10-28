// <copyright file="ChildTransactionEntity.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Child transaction entity
    /// </summary>
    public class ChildTransactionEntity : Entity<Guid>
    {
        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}