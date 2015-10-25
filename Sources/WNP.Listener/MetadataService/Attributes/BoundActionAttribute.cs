// <copyright file="BoundActionAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Attributes
{
    using System;

    /// <summary>
    /// Attribute to mark method as OData bound Action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class BoundActionAttribute : Attribute
    {
    }
}