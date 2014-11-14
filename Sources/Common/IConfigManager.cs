//-----------------------------------------------------------------------
// <copyright file="IConfigManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Lookup;

    /// <summary>
    /// Interface for reading and writing Listener configuration settings.
    /// </summary>
    public interface IConfigManager
    {
        /// <summary>
        /// Gets the configuration setting value as string.
        /// </summary>
        /// <param name="configSettingName">Name of the configuration setting.</param>
        /// <returns>
        /// Configuration setting value.
        /// </returns>
        string GetConfigSettingValue(ConfigSettingNameLookup configSettingName);

        /// <summary>
        /// Sets the configuration setting value.
        /// </summary>
        /// <param name="configSettingName">The configuration setting name.</param>
        /// <param name="configSettingValue">The configuration setting value.</param>
        void SetConfigSettingValue(ConfigSettingNameLookup configSettingName, string configSettingValue);

        /// <summary>
        /// Determines whether listener web service is enabled in configuration.
        /// </summary>
        /// <returns>
        /// True if listener web service is enabled, false if disabled.
        /// </returns>
        bool IsListenerEnabled();

        /// <summary>
        /// Determines whether notifications are enabled in configuration.
        /// </summary>
        /// <returns>
        /// True if notifications are enabled, false if disabled.
        /// </returns>
        bool IsNotificationsEnabled();
    }
}
