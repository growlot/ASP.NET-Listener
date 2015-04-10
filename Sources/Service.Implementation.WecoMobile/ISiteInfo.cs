//-----------------------------------------------------------------------
// <copyright file="ISiteInfo.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Service.Contract;

    /// <summary>
    /// Interface for web service that will communicate with Weco Mobile application
    /// </summary>
    [ServiceContract]
    public interface ISiteInfo
    {
        /// <summary>
        /// Web service contract for receiving site information.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// The site information
        /// </returns>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        Site GetSiteInfo(SiteInfoRequest request);

        /// <summary>
        /// Adds the meter test results.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void AddMeterTestResults(MeterTestResultsRequest request);

        /// <summary>
        /// Adds the related files to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void AddRelatedFiles(AddRelatedFilesRequest request);

        /// <summary>
        /// Removes the related files from site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void RemoveRelatedFiles(RemoveRelatedFilesRequest request);

        /// <summary>
        /// Updates specified circuit location data.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void UpdateCircuitLocation(UpdateCircuitLocationRequest request);

        /// <summary>
        /// Adds the comments to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void AddComments(AddCommentsRequest request);

        /// <summary>
        /// Removes the comments to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void RemoveComments(RemoveCommentsRequest request);
        
        /// <summary>
        /// Updates the comments for site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContractAttribute(typeof(ServiceFaultDetails))]
        void UpdateComments(UpdateCommentsRequest request);
    }
}
