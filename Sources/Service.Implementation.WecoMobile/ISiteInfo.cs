//-----------------------------------------------------------------------
// <copyright file="ISiteInfo.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.WecoMobile
{
    using System.ServiceModel;
    using Contract;
    using ListenerModel = Common.Model;

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
        [FaultContract(typeof(ServiceFaultDetails))]
        ListenerModel.Site GetSiteInfo(SiteInfoRequest request);

        /// <summary>
        /// Checks out device to specified user and assigns it to specified truck.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The checked out device</returns>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        ListenerModel.Device CheckoutDevice(CheckoutRequest request);

        /// <summary>
        /// Adds the meter test results.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void AddMeterTestResults(MeterTestResultsRequest request);

        /// <summary>
        /// Adds the related files to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void AddRelatedFiles(AddRelatedFilesRequest request);

        /// <summary>
        /// Removes the related files from site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void RemoveRelatedFiles(RemoveRelatedFilesRequest request);

        /// <summary>
        /// Updates specified circuit location data.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void UpdateCircuitLocation(UpdateCircuitLocationRequest request);

        /// <summary>
        /// Adds the comments to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void AddComments(AddCommentsRequest request);

        /// <summary>
        /// Removes the comments to site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void RemoveComments(RemoveCommentsRequest request);

        /// <summary>
        /// Updates the comments for site and/or device.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [FaultContract(typeof(ServiceFaultDetails))]
        void UpdateComments(UpdateCommentsRequest request);
    }
}
