// <copyright file="WithinListenerContextAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Core
{
    using System;

    /// <summary>
    /// Indicates, that the class operates within listener context
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class WithinListenerContextAttribute : Attribute
    {
    }
}