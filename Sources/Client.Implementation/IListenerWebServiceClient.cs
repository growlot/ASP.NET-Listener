//-----------------------------------------------------------------------
// <copyright file="IListenerWebServiceClient.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Client.Implementation
{
    using System.Resources;
    using AMSLLC.Listener.Client.Implementation.Messages;

    /// <summary>
    /// Interface for accessing Listener web service.
    /// </summary>
    public interface IListenerWebServiceClient
    {
        /// <summary>
        /// Calls web service to retrieve device information.
        /// </summary>
        /// <param name="request">The device retrieve request message.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        ClientResponse GetDeviceData(GetDeviceRequest request);

        /// <summary>
        /// Calls web service to publish device information.
        /// </summary>
        /// <param name="request">The device information needed to retrieve it from database.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        ClientResponse SendDeviceData(DeviceRequest request);

        /// <summary>
        /// Call web service to publish device test results
        /// </summary>
        /// <param name="request">The device test information needed to retrieve it from database.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        ClientResponse SendDeviceTestData(DeviceTestRequest request);

        /// <summary>
        /// Call web service to publish batch information
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        ClientResponse SendBatchData(BatchRequest request);
     }
}
