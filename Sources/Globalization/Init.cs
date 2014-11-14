//-----------------------------------------------------------------------
// <copyright file="Init.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Globalization
{
    using System.Reflection;
    using System.Resources;

    /// <summary>
    /// Initializes string manager to access language specific texts.
    /// </summary>
    public static class Init
    {
        /// <summary>
        /// The string manager
        /// </summary>
        public static readonly ResourceManager StringManager = new ResourceManager("AMSLLC.Listener.Globalization.Properties.Resources", Assembly.GetExecutingAssembly());
    }
}
