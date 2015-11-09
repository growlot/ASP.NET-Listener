// <copyright file="IBoundActionsContainer.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService
{
    /// <summary>
    /// Interface is used to mark class as containing entity actions.
    /// Currently is used only in conjunction with WNP Controllers.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker interface")]
    public interface IBoundActionsContainer : IWNPEntityController
    {
    }
}