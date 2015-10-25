// <copyright file="ActionPrefixAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.MetadataService.Attributes
{
    using System;

    /// <summary>
    /// This attribute is used to define custom prefix for generated entity action (AMSLLC.Listener.{Prefix}_{ActionName}).
    /// By default prefix is class name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ActionPrefixAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActionPrefixAttribute"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        public ActionPrefixAttribute(string prefix)
        {
            this.Prefix = prefix;
        }

        /// <summary>
        /// Gets the prefix.
        /// </summary>
        /// <value>
        /// The prefix.
        /// </value>
        public string Prefix { get; }
    }
}