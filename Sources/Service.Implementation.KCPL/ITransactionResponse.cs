//-----------------------------------------------------------------------
// <copyright file="ITransactionResponse.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using AMSLLC.Listener.Service.Implementation.MessageBasedSoap;

    /// <summary>
    /// SOAP interface for transaction response service
    /// </summary>
    [ServiceContract(Namespace = "")]
    [DispatchByBodyElementBehavior]
    public interface ITransactionResponse
    {
        /// <summary>
        /// Web service contract for receiving transaction responses from ODM.
        /// </summary>
        /// <param name="request">The request.</param>
        [OperationContract]
        [DispatchBodyElement("TransactionResponseServiceRequest", "")]
        void TransactionResponse(Message request);

        /*
         * ODM web service mocks
         * 
        /// <summary>
        /// Load the asset.
        /// </summary>
        /// <param name="AssetLoadServiceRequest">The service request to load the asset.</param>
        [OperationContract]
        [DispatchBodyElement("AssetLoadServiceRequest", "")]
        void AssetLoad(Message AssetLoadServiceRequest);

        /// <summary>
        /// Update the asset.
        /// </summary>
        /// <param name="AssetUpdateServiceRequest">The service request to update the asset.</param>
        [OperationContract]
        [DispatchBodyElement("AssetUpdateServiceRequest", "")]
        void AssetUpdate(Message AssetUpdateServiceRequest);

        /// <summary>
        /// The test result.
        /// </summary>
        /// <param name="TestResultServiceRequest">The service request to save test result.</param>
        [OperationContract]
        [DispatchBodyElement("TestResultServiceRequest", "")]
        void TestResult(Message TestResultServiceRequest);
        */
    }
}
