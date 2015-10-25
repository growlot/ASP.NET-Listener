// <copyright file="BoundEntityKeyAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Attributes
{
    using System;

    /// <summary>
    /// Attribute to mark parameter as bound action key attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class BoundEntityKeyAttribute : Attribute
    {
    }
}