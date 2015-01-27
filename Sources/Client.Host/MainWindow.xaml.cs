//-----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Host
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Globalization;
    using log4net.Config;
    
    /// <summary>
    /// Interaction logic for application
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The string manager
        /// </summary>
        private ResourceManager stringManager;

        /// <summary>
        /// The listener web service
        /// </summary>
        private IListenerWebServiceClient webService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            XmlConfigurator.Configure();
            this.InitializeComponent();
            this.webService = new ListenerWebServiceClient();
            this.stringManager = Init.StringManager;
        }

        /// <summary>
        /// Retrieves device data.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DeviceReceive(object sender, RoutedEventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            ClientResponse response = null;

            GetDeviceRequest request = new GetDeviceRequest()
            {
                ListenerUrl = new Uri(ServiceAddress.Text),
                CompanyId = 0,
                EquipmentNumber = enteredEquipmenNumberDRT.Text,
                ServiceType = ((ComboBoxItem)enteredServiceTypeDRT.SelectedItem).Tag.ToString(),
                EquipmentType = ((ComboBoxItem)enteredEqipmentTypeDRT.SelectedItem).Tag.ToString(),
                Location = "VR7",
                TesterId = "Vladas",
                TestStandard = "TS"
            };
            response = this.webService.GetDeviceData(request);

            if (response != null && response.ReturnCode == 0)
            {
                // service call succeeded
                this.serviceCallStatus.Text = this.stringManager.GetString("ServiceCallOk", CultureInfo.CurrentCulture);
            }
            else
            {
                this.serviceCallStatus.Text = this.stringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture) + response.Message;
            }

            watch.Stop();
            requestDuration.Text = watch.Elapsed.ToString();
        }

        /// <summary>
        /// Sends devices test results.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void DeviceShopTest(object sender, RoutedEventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            
            DeviceTestRequest request = new DeviceTestRequest()
            {
                ListenerUrl = new Uri(ServiceAddress.Text),
                CompanyId = 1,
                EquipmentNumber = enteredEquipmenNumberDST.Text,
                ServiceType = ((ComboBoxItem)enteredServiceTypeDST.SelectedItem).Tag.ToString(),
                EquipmentType = ((ComboBoxItem)enteredEqipmentTypeDST.SelectedItem).Tag.ToString(),
                TestDate = DateTime.Parse(enteredTestDateDST.Text, CultureInfo.InvariantCulture)
            };

            ClientResponse response = this.webService.SendDeviceTestData(request);

            if (response.ReturnCode == 0)
            {
                // service call succeeded
                this.serviceCallStatus.Text = this.stringManager.GetString("ServiceCallOk", CultureInfo.CurrentCulture);
            }
            else
            {
                this.serviceCallStatus.Text = this.stringManager.GetString("ServiceCallFailed", CultureInfo.CurrentCulture) + response.Message;
            }

            watch.Stop();
            requestDuration.Text = watch.Elapsed.ToString();
        }

        /// <summary>
        /// Exits the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
