//-----------------------------------------------------------------------
// <copyright file="ISaga.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.ApplicationService
{
    /// <summary>
    /// Interface for all sagas.
    /// </summary>
    /// <remarks>
    /// A persistent object that turns domain events into commands. Stateful and behaviorful.
    /// The only state is what's required for knowing when to create the commands.
    /// </remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is a marker interface.")]
    public interface ISaga
    {
    }
}
