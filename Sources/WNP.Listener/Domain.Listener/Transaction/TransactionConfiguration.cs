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
    public class TransactionConfiguration : Entity<int>, IAggregateRoot, IOriginator
    {
        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        /// <exception cref="NotImplementedException"></exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "memento", Justification = "Wireframing, cleanup this later")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Wireframing, cleanup this later")]
        void IOriginator.SetMemento(IMemento memento)
        {
            this.SetMemento(memento);
        }

        /// <summary>
        /// Sets the memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters",
            MessageId = "memento", Justification = "Wireframing, cleanup this later")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "Wireframing, cleanup this later")]
        protected void SetMemento(IMemento memento)
        {
        }
    }
}