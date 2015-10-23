// <copyright file="CollectionWideActionAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.Actions.Attributes
{
    using System;

    /// <summary>
    /// Attribute used to mark OData bound Actions that are bound to entity collection rather than to entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CollectionWideActionAttribute : Attribute
    {
    }
}