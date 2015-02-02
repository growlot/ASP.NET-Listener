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
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceProcess;
    using System.Text;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Service.Implementation;
    using log4net;

    /// <summary>
    /// Controls all services hosted by this project.
    /// </summary>
    public partial class CustomServiceHost : ServiceBase
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// The list of service hosts
        /// </summary>
        private IList<ServiceHost> serviceHosts = new List<ServiceHost>();

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
            // Initializes services based on the customer setting in application config file
            // Need to put actuall references to separate methods, because otherwize C# tries 
            // to load the assembly even if it is not called because of conditional logic
            if (ConfigurationManager.AppSettings["Customer"].Contains("Alliant"))
            {
                this.LoadAlliant();
            }

            if (ConfigurationManager.AppSettings["Customer"].Contains("KCP&L"))
            {
                this.LoadKCPL();
            }

            if (ConfigurationManager.AppSettings["Customer"].Contains("Core"))
            {
                this.OpenHost<ServiceCore>();
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
        /// <typeparam name="T">Type of the service that must be opened.</typeparam>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "False positive")]
        private void OpenHost<T>()
        {
            Type type = typeof(T);
            ServiceHost serviceHost = null;
            try
            {
                serviceHost = new ServiceHost(type);
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

        /// <summary>
        /// Loads the alliant web services.
        /// </summary>
        private void LoadAlliant()
        {
            this.OpenHost<AMSLLC.Listener.Service.Implementation.Alliant.CustomService>();
            this.OpenHost<AMSLLC.Listener.Service.Implementation.Alliant.UpdateClassificationCode>();
        }

        /// <summary>
        /// Loads the KCPL web services.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1806:DoNotIgnoreMethodResults", MessageId = "System.Boolean.TryParse(System.String,System.Boolean@)", Justification = "In case TryParse fails, initialProcessing will be set to false. This is desired value")]
        private void LoadKCPL()
        {
            this.OpenHost<AMSLLC.Listener.Service.Implementation.KCPL.CustomService>();
            this.OpenHost<AMSLLC.Listener.Service.Implementation.KCPL.TransactionResponseService>();

            bool initialProcessing;
            bool.TryParse(ConfigurationManager.AppSettings["Kcpl.InitialProcessingOn"], out initialProcessing);

            if (initialProcessing)
            {
                AMSLLC.Listener.Service.Implementation.KCPL.CustomService service = new Implementation.KCPL.CustomService();
                service.ProcessInitialLoad();
            } 
        }
    }
}
