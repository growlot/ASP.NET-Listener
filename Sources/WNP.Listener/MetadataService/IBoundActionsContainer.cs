// <copyright file="IBoundActionsContainer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Interface is used to mark class as containing entity actions.
    /// Currently is used only in conjunction with WNPController.
    /// </summary>
    public interface IBoundActionsContainer
    {
        /// <summary>
        /// The specifics of this method is that it can (and will) be invoked without
        /// creating initialized class instance. Thus the implementation should not rely on
        /// any constructor-related logic.
        /// </summary>
        /// <returns>Table name in lower case (with schema)</returns>
        string GetEntityTableName();
    }
}