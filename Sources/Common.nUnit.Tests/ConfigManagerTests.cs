//-----------------------------------------------------------------------
// <copyright file="ConfigSettingsTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Common.Unit.Tests
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests <see cref="ConfigManager"/> class.
    /// </summary>
    [TestClass]
    public class ConfigManagerTests
    {
        /// <summary>
        /// Tests if config manager throws exception if it is initialized without persistence controller
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ConfigManagerInitializationShouldThrowExceptionIfPersistenceManagerIsNotSpecified()
        {
            IConfigManager settings;

            settings = new ConfigManager(null);
            settings.ToString();
        }

        /// <summary>
        /// Tests if config manager can be initialized if no methods are overrided
        /// </summary>
        [TestMethod]
        public void ForTestingPurposesConfigManagerShouldInitializeWithoutAnyOverrides()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
                settings.ToString();
            }
        }

        /// <summary>
        /// Tests if Is*Enabled returns true.
        /// </summary>
        [TestMethod]
        public void IsEnabledShouldReturnTrueIfTrueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("listener.enabled", "True"));
                    configs.Add(new Config("notifications.enabled", "True"));
                    return configs;
                });
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            Assert.AreEqual(true, settings.IsListenerEnabled());
            Assert.AreEqual(true, settings.IsNotificationsEnabled());
        }

        /// <summary>
        /// Tests if Is*Enabled returns false.
        /// </summary>
        [TestMethod]
        public void IsEnabledShouldReturnFalseIfFalseIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("listener.enabled", "False"));
                    configs.Add(new Config("notifications.enabled", "False"));
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }
            
            Assert.AreEqual(false, settings.IsListenerEnabled());
            Assert.AreEqual(false, settings.IsNotificationsEnabled());
        }

        /// <summary>
        /// Tests if IsListenerEnabled returns exception when persisted value is not "True" or "False"
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void IsListenerEnabledShouldReturnExceptionIfNonBooleanValueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("listener.enabled", "Disabled"));
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            settings.IsListenerEnabled();
        }

        /// <summary>
        /// Tests if IsListenerEnabled returns exception when persisted value is not "True" or "False"
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void IsListenerEnabledShouldReturnExceptionIfNullValueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("listener.enabled", null));
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            settings.IsListenerEnabled();
        }

        /// <summary>
        /// Tests if IsNotificationsEnabled returns exception when persisted value is not "True" or "False"
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void IsNotificationsEnabledShouldReturnExceptionIfNonBooleanValueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("notifications.enabled", "1"));
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            settings.IsNotificationsEnabled();
        }

        /// <summary>
        /// Tests if Is*Enabled returns false when there is no value persisted
        /// </summary>
        [TestMethod]
        public void IsEnabledShouldReturnFalseIfNoValueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            Assert.AreEqual(false, settings.IsListenerEnabled());
            Assert.AreEqual(false, settings.IsNotificationsEnabled());
        }

        /// <summary>
        /// Tests if config value is returns correctly.
        /// </summary>
        [TestMethod]
        public void GetConfigSettingValueShouldGetPersistedValue()
        {
            string url = "http://listener.url";
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    configs.Add(new Config("listener.url", url));
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            Assert.AreEqual(url, settings.GetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl));
        }

        /// <summary>
        /// Tests if empty string is returned if setting was not persisted.
        /// </summary>
        [TestMethod]
        public void GetConfigSettingValueShouldGetEmptyStringIfNoValueIsPersisted()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    IList<Config> configs = new List<Config>();
                    return configs;
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            Assert.AreEqual(string.Empty, settings.GetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl));
        }

        /// <summary>
        /// Tests if config value is stored and read correctly.
        /// </summary>
        [TestMethod]
        public void NewValueShouldBeReadAfterSetConfigSettingValue()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveAllOf1SessionAction<Config>((sessionAction) =>
                {
                    return new List<Config>();
                });

                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            string url = "http://listener.url";
            settings.SetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl, url);
            Assert.AreEqual(url, settings.GetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl));

            url = "http://listener.url.updated";
            settings.SetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl, url);
            Assert.AreEqual(url, settings.GetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl));
        }
        
        /// <summary>
        /// Tests if excpetion is thrown if config value is not specified (null).
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ExceptionShouldBeThrownIfConfigValueIsNull()
        {
            IConfigManager settings;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                IPersistenceController persistenceController = new PersistenceController();
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                settings = new ConfigManager(persistenceController);
            }

            settings.SetConfigSettingValue(ConfigSettingNameLookup.ListenerUrl, null);
        }
    }
}
