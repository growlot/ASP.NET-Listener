// <copyright file="EntityNotFoundException.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler
{
    using System;

    /// <summary>
    /// The exception that is thrown if no entity found on single entity request.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
    }
}