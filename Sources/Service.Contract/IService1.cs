//-----------------------------------------------------------------------
// <copyright file="IService1.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope = "Namespace", Justification = "DataContract properties generate false positives")]

    /// <summary>
    /// SOAP interface for for Listener service
    /// </summary>
    [ServiceContract]
    public interface IService1
    {
        /// <summary>
        /// Web service contract for receiving device information.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void GetDevice(GetDeviceServiceRequest request);

        /// <summary>
        /// Web service contract for sending device information.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void SendDevice(SendDataServiceRequest request);

        /// <summary>
        /// Web service contract for sending device test results.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void SendTestData(SendDataServiceRequest request);

        /// <summary>
        /// Web service contract for sending batch results.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void SendBatch(SendDataServiceRequest request);
    }
}
