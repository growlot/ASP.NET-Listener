// //-----------------------------------------------------------------------
// // <copyright file="TransactionConfiguration.cs" company="Advanced Metering Services LLC">
// //     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Domain.Listener.Transaction
{
    using System;

    /// <summary>
    /// Transaction configuration
    /// </summary>
    public class TransactionConfiguration : Entity<int>, IAggregateRoot
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