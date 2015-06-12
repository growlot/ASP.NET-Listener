//-----------------------------------------------------------------------
// <copyright file="ServiceHostWithCustomConfig.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Host
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.ServiceModel;

    /// <summary>
    /// Custom WCF service host that allows to inject custom config file.
    /// Implementation based on <see href="http://blogs.msdn.com/b/dotnetinterop/archive/2008/09/22/custom-service-config-file-for-a-wcf-service-hosted-in-iis.aspx">this article</see>
    /// </summary>
    public class ServiceHostWithCustomConfig : ServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostWithCustomConfig"/> class.
        /// </summary>
        /// <param name="serviceType">The type of hosted service.</param>
        /// <param name="baseAddresses">An array of type <see cref="T:System.Uri" /> that contains the base addresses for the hosted service.</param>
        public ServiceHostWithCustomConfig(Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceHostWithCustomConfig" /> class.
        /// </summary>
        /// <param name="singletonInstance">The instance of the hosted service.</param>
        /// <param name="baseAddresses">The base addresses.</param>
        public ServiceHostWithCustomConfig(object singletonInstance, params Uri[] baseAddresses)
            : base(singletonInstance, baseAddresses)
        {
        }

        /// <summary>
        /// Loads the service description information from the configuration file and applies it to the runtime being constructed.
        /// </summary>
        protected override void ApplyConfiguration()
        {
            // generate the name of the custom configFile, from the service name:
            string configFilename = string.Format(CultureInfo.InvariantCulture, "{0}.config", this.Description.ServiceType.Assembly.Location);

            if (string.IsNullOrEmpty(configFilename) || !File.Exists(configFilename))
            {
                base.ApplyConfiguration();
            }
            else
            {
                this.LoadConfigFromCustomLocation(configFilename);
            }
        }
        
        /// <summary>
        /// Loads the configuration from custom location.
        /// </summary>
        /// <param name="configFilename">The configuration filename.</param>
        /// <exception cref="System.ArgumentException">ServiceElement doesn't exist</exception>
        private void LoadConfigFromCustomLocation(string configFilename)
        {
            var filemap = new System.Configuration.ExeConfigurationFileMap();
            filemap.ExeConfigFilename = configFilename;

            System.Configuration.Configuration config =
            System.Configuration.ConfigurationManager.OpenMappedExeConfiguration(filemap, System.Configuration.ConfigurationUserLevel.None);

            var serviceModel = System.ServiceModel.Configuration.ServiceModelSectionGroup.GetSectionGroup(config);

            bool loaded = false;
            foreach (System.ServiceModel.Configuration.ServiceElement se in serviceModel.Services.Services)
            {
                if (!loaded)
                {
                    if (se.Name == this.Description.ConfigurationName)
                    {
                        this.LoadConfigurationSection(se);
                        loaded = true;
                    }
                }
            }

            if (!loaded)
            {
                throw new ArgumentException("ServiceElement doesn't exist");
            }
        }
    }
}
