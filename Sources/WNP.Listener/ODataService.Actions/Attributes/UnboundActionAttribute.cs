// <copyright file="UnboundActionAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Actions.Attributes
{
    using System;

    /// <summary>
    /// Attribute to mark method as OData unbound Action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class UnboundActionAttribute : Attribute
    {
    }
}