//-----------------------------------------------------------------------
// <copyright file="CustomServiceHost.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Host
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Composition;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Service.Contract;
    using log4net;

    /// <summary>
    /// Controls all services hosted by this project.
    /// </summary>
    [Export(typeof(CustomServiceHost))]
    public partial class CustomServiceHost : ServiceBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// The list of service hosts
        /// </summary>
        private IList<ServiceHostWithCustomConfig> serviceHosts = new List<ServiceHostWithCustomConfig>();

        /// <summary>
        /// The alliant service
        /// </summary>
        #pragma warning disable 0649
        [ImportMany]
        private IEnumerable<Lazy<IPluginService, INamedPluginMetadata>> servicePlugins;
        #pragma warning restore 0649

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomServiceHost"/> class.
        /// </summary>
        public CustomServiceHost()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Starts all services.
        /// </summary>
        public void OpenAll()
        {
            foreach (Lazy<IPluginService, INamedPluginMetadata> plugin in this.servicePlugins)
            {
                foreach (Type type in plugin.Value.ServiceTypes)
                {
                    this.OpenHost(type);
                }
            }

            //// this.OpenHost<Service.Implementation.Service1Rest>();
        }

        /// <summary>
        /// Closes all running services.
        /// </summary>
        public void CloseAll()
        {
            foreach (ServiceHost serviceHost in this.serviceHosts)
            {
                if (serviceHost != null)
                {
                    serviceHost.Close();
                }
            }
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Start command is sent to the service by the Service Control Manager (SCM) or when the operating system starts (for a service that starts automatically). Specifies actions to take when the service starts.
        /// </summary>
        /// <param name="args">Data passed by the start command.</param>
        protected override void OnStart(string[] args)
        {
            this.OpenAll();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            this.CloseAll();
        }

        /// <summary>
        /// Opens service host and adds it to the list of hosted services.
        /// </summary>
        /// <param name="type">The type.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "False positive")]
        private void OpenHost(Type type)
        {
            ServiceHostWithCustomConfig serviceHost = null;
            try
            {
                serviceHost = new ServiceHostWithCustomConfig(type);
                serviceHost.Open();
                this.serviceHosts.Add(serviceHost);
                serviceHost = null;
            }
            catch (Exception ex)
            {
                Logger.Error("Web service host startup failed.", ex);
                throw;
            }
            finally
            {
                if (serviceHost != null)
                {
                    serviceHost.Close();
                }
            }
        }

        /////// <summary>
        /////// Loads the KCPL web services.
        /////// </summary>
        ////[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)", Justification = "In case TryParse fails, initialProcessing will be set to false. This is desired value")]
        ////private void LoadKCPL()
        ////{
        ////    bool initialProcessing;
        ////    bool.TryParse(ConfigurationManager.AppSettings["Kcpl.InitialProcessingOn"], out initialProcessing);

        ////    if (initialProcessing)
        ////    {
        ////        AMSLLC.Listener.Service.Implementation.KCPL.CustomService service = new Implementation.KCPL.CustomService();
        ////        service.ProcessInitialLoad();
        ////    } 
        ////}
    }
}
