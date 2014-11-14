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
        /// <exception cref="System.ArgumentNullException">request;Can not retrieve device information when request is not specified.</exception>
        ClientResponse GetDevice(GetDeviceRequest request);

        /// <summary>
        /// Call web service to publish device test results
        /// </summary>
        /// <param name="request">The device test information needed to retrieve it from database.</param>
        /// <returns>
        /// Response detailing if call succeeded.
        /// </returns>
        /// <exception cref="ArgumentNullException">request;Can not send device test information when request is not specified.</exception>
        ClientResponse SendDeviceTest(SendDeviceTestRequest request);
    }
}
