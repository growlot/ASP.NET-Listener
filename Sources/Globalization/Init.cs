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
        private static readonly ResourceManager CommonStringManager = new ResourceManager("AMSLLC.Listener.Globalization.Properties.Resources", Assembly.GetExecutingAssembly());

        /// <summary>
        /// Gets the string manager.
        /// </summary>
        /// <value>
        /// The string manager.
        /// </value>
        public static ResourceManager StringManager
        {
            get
            {
                return CommonStringManager;
            }
        }
    }
}
