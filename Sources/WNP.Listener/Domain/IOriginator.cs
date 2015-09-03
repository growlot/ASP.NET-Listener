//-----------------------------------------------------------------------
// <copyright file="IOriginator.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Domain
{
    /// <summary>
    /// Implements originator in memento pattern
    /// </summary>
    public interface IOriginator
    {
        /// <summary>
        /// Restores objects state from provided memento.
        /// </summary>
        /// <param name="memento">The memento.</param>
        void SetMemento(IMemento memento);
    }
}
