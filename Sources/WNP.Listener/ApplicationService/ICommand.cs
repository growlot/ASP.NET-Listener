//-----------------------------------------------------------------------
// <copyright file="ICommand.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService
{
    /// <summary>
    /// Marker interface for all application commands.
    /// </summary>
    /// <remarks>
    /// A simple message that represents a user's request to make something happen.
    /// Just data with no behavior and often named in the imperative tense.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is a marker interface.")]
    public interface ICommand
    {
    }
}
