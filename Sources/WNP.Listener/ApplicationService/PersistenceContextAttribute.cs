// <copyright file="PersistenceContextAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ApplicationService
{
    using System;

    /// <summary>
    /// Attribute marking persistence context property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class PersistenceContextAttribute : Attribute
    {
    }
}