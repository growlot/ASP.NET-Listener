//-----------------------------------------------------------------------
// <copyright file="ConfigManager.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Lookup;
    using log4net;

    /// <summary>
    /// Controls reading and writing Listener configuration settings to database.
    /// </summary>
    public class ConfigManager : IConfigManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The listener system
        /// </summary>
        private ListenerClientSystem listenerClientSystem;
        
        /// <summary>
        /// The configuration settings read from database
        /// </summary>
        private IDictionary<string, string> config;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigManager" /> class.
        /// </summary>
        /// <param name="persistenceController">The persistence controller.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when persistence controllers is not provided (null).
        /// </exception>
        public ConfigManager(IPersistenceController persistenceController)
        {
            if (persistenceController == null)
            {
                string exceptionMessage = "ConfigManager class can not be created because persistenceController is null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("persistenceController", exceptionMessage);
            }

            this.listenerClientSystem = persistenceController.ListenerClientSystem;
            this.LoadConfig();
        }

        /// <summary>
        /// Gets the configuration setting value as string.
        /// </summary>
        /// <param name="configSettingName">The setting.</param>
        /// <returns>
        /// Configuration setting value.
        /// </returns>
        public string GetConfigSettingValue(ConfigSettingNameLookup configSettingName)
        {
            return this.GetConfigSettingValue(configSettingName, string.Empty);
        }

        /// <summary>
        /// Sets the configuration setting value.
        /// </summary>
        /// <param name="configSettingName">The configuration setting name.</param>
        /// <param name="configSettingValue">The configuration setting value.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when config setting value is not provided (null).
        /// </exception>
        public void SetConfigSettingValue(ConfigSettingNameLookup configSettingName, string configSettingValue)
        {
            if (configSettingValue == null)
            {
                string exceptionMessage = "Config setting value can not be null.";
                Logger.Error(exceptionMessage);
                throw new ArgumentNullException("configSettingValue", exceptionMessage);
            }
            
            string settingName = Utilities.GetEnumDescription(configSettingName);

            if (this.config.ContainsKey(settingName))
            {
                this.listenerClientSystem.UpdateConfigSetting(settingName, configSettingValue);
            }
            else
            {
                this.listenerClientSystem.AddConfigSetting(settingName, configSettingValue);
            }

            this.config = this.listenerClientSystem.ConfigElements;
        }

        /// <summary>
        /// Determines whether listener web service is enabled in configuration.
        /// </summary>
        /// <returns>
        /// True if listener web service is enabled, false if disabled.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when it is not possible to determine if listener web service is enabled.
        /// </exception>
        public bool IsListenerEnabled()
        {
            bool result = false;
            try
            {
                result = this.GetConfigSettingValueAsBoolean(ConfigSettingNameLookup.ListenerEnabled);
            }
            catch (InvalidOperationException exception)
            {
                string exceptionMessage = "Unable to determine if listener web service is enabled.";
                Logger.Error(exceptionMessage, exception);
                throw new InvalidOperationException(exceptionMessage, exception);
            }

            return result;
        }

        /// <summary>
        /// Determines whether notifications are enabled in configuration.
        /// </summary>
        /// <returns>
        /// True if notifications are enabled, false if disabled.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when it is not possible to determine if notifications are enabled.
        /// </exception>
        public bool IsNotificationsEnabled()
        {
            bool result = false;
            try
            {
                result = this.GetConfigSettingValueAsBoolean(ConfigSettingNameLookup.NotificationsEnabled);
            }
            catch (InvalidOperationException exception)
            {
                string exceptionMessage = "Unable to determine if notifications are enabled.";
                Logger.Error(exceptionMessage, exception);
                throw new InvalidOperationException(exceptionMessage, exception);
            }

            return result;
        }
                
        /// <summary>
        /// Gets the configuration setting value as boolean.
        /// </summary>
        /// <param name="configSettingName">The setting.</param>
        /// <returns>
        /// Configuration setting value, or false if setting had non boolean value.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown when setting read from database can not be converted to boolean value. 
        /// </exception>
        private bool GetConfigSettingValueAsBoolean(ConfigSettingNameLookup configSettingName)
        {
            string stringResult;
            bool boolResult = false;

            stringResult = this.GetConfigSettingValue(configSettingName, "False");

            try
            {
                boolResult = bool.Parse(stringResult);
            }
            catch (ArgumentException exception)
            {
                string settingName = Utilities.GetEnumDescription(configSettingName);
                string exceptionMessage = string.Format(CultureInfo.InvariantCulture, "Config setting '{0}' value is null.{1}Cannot parse a null string to boolean.", settingName, Environment.NewLine);
                Logger.Error(exceptionMessage, exception);
                throw new InvalidOperationException(exceptionMessage, exception);
            }
            catch (FormatException exception)
            {
                string settingName = Utilities.GetEnumDescription(configSettingName);
                string exceptionMessage = string.Format(CultureInfo.InvariantCulture, "Config setting '{0}' value is '{1}'.{2}Cannot parse '{1}' to boolean", settingName, stringResult, Environment.NewLine);
                Logger.Error(exceptionMessage, exception);
                throw new InvalidOperationException(exceptionMessage, exception);
            }

            return boolResult;
        }

        /// <summary>
        /// Loads the configuration from database.
        /// </summary>
        private void LoadConfig()
        {
            this.config = this.listenerClientSystem.ConfigElements;
        }

        /// <summary>
        /// Gets the configuration setting value for specified configuration setting name.
        /// </summary>
        /// <param name="configSettingName">Name of the configuration setting.</param>
        /// <param name="defaultValue">The default value. Used if configuration setting wasn't found in the database.</param>
        /// <returns>Configuration setting value read from database, or <paramref name="defaultValue"/> if it wasn't found.</returns>
        private string GetConfigSettingValue(ConfigSettingNameLookup configSettingName, string defaultValue)
        {
            string result;
            string settingName = Utilities.GetEnumDescription(configSettingName);

            if (this.config.ContainsKey(settingName))
            {
                result = this.config[settingName];
            }
            else
            {
                this.listenerClientSystem.AddConfigSetting(settingName, defaultValue);
                this.LoadConfig();
                result = defaultValue;
            }

            return result;
        }
    }
}
