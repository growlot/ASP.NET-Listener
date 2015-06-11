//-----------------------------------------------------------------------
// <copyright file="ExportNamedPluginAttribute.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    /// <summary>
    /// Attribute that allows to export MEF part with name metadata.
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ExportNamedPluginAttribute : ExportAttribute, INamedPluginMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExportNamedPluginAttribute" /> class.
        /// </summary>
        /// <param name="contractType">Type of the exported contract.</param>
        /// <param name="name">The name.</param>
        public ExportNamedPluginAttribute(Type contractType, string name)
            : base(contractType)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets the name of plugin.
        /// </summary>
        /// <value>
        /// The plugin name.
        /// </value>
        public string Name { get; private set; }
    }
}
