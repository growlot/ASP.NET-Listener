//-----------------------------------------------------------------------
// <copyright file="ListenerClientSystem.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AMSLLC.Listener.Common.Model;
    using NHibernate.Criterion;

    /// <summary>
    /// Manages all business objects related to Listener client.
    /// </summary>
    public class ListenerClientSystem
    {
        /// <summary>
        /// The persistence manager
        /// </summary>
        private IPersistenceManager persistenceManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListenerClientSystem" /> class.
        /// </summary>
        /// <param name="persistenceManager">The persistence manager.</param>
        /// <exception cref="System.ArgumentNullException">persistenceManager;Can't initialize ListenerClientSystem without persistenceManager.</exception>
        public ListenerClientSystem(IPersistenceManager persistenceManager)
        {
            if (persistenceManager == null)
            {
                throw new ArgumentNullException("persistenceManager", "Can't initialize ListenerClientSystem without persistenceManager.");
            }

            this.persistenceManager = persistenceManager;

            this.Config = new List<Config>();
            IList<Config> retrievedConfigs = this.persistenceManager.RetrieveAll<Config>(SessionAction.BeginAndEnd);
            if (retrievedConfigs != null)
            {
                ((List<Config>)this.Config).AddRange(retrievedConfigs);
            }

            this.ConfigElements = this.Config.ToDictionary(Item => Item.Name, Item => Item.Value);
        }

        /// <summary>
        /// Gets the list of configuration settings.
        /// </summary>
        /// <value>
        /// The list of configuration settings.
        /// </value>
        public IList<Config> Config { get; private set; }

        /// <summary>
        /// Gets the configuration elements in a dictionary.
        /// </summary>
        /// <value>
        /// The configuration elements dictionary.
        /// </value>
        public IDictionary<string, string> ConfigElements { get; private set; }

        /// <summary>
        /// Adds the configuration setting.
        /// </summary>
        /// <param name="configSettingName">Name of the configuration setting.</param>
        /// <param name="configSettingValue">The configuration setting value.</param>
        public void AddConfigSetting(string configSettingName, string configSettingValue)
        {
            Config config = new Config();
            config.Name = configSettingName;
            config.Value = configSettingValue;

            this.Config.Add(config);
            this.ConfigElements.Add(config.Name, config.Value);

            this.persistenceManager.Save<Config>(config);
        }

        /// <summary>
        /// Updates the value of configuration setting.
        /// </summary>
        /// <param name="configSettingName">Name of the configuration setting.</param>
        /// <param name="configSettingValue">The configuration setting value.</param>
        public void UpdateConfigSetting(string configSettingName, string configSettingValue)
        {
            Config updatedConfig = null;
            this.ConfigElements[configSettingName] = configSettingValue;

            updatedConfig = ((List<Config>)this.Config).Find(x => x.Name == configSettingName);
            updatedConfig.Value = configSettingValue;

            this.persistenceManager.Save<Config>(updatedConfig);
        }
    }
}
