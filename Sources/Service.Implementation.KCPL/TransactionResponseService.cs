//-----------------------------------------------------------------------
// <copyright file="TransactionResponseService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL
{
    using System;
    using System.Configuration;
    using System.Resources;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using System.Xml;
    using System.Xml.Serialization;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Globalization;
    using AMSLLC.Listener.Service.Implementation.KCPL.Messages;
    using AMSLLC.Listener.Service.Implementation.MessageBasedSoap;
    using log4net;

    /// <summary>
    /// Implements transaction response web service.
    /// </summary>
    public class TransactionResponseService : ITransactionResponse
    {
        /////// <summary>
        /////// The logger
        /////// </summary>
        ////// private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /////// <summary>
        /////// The string manager
        /////// </summary>
        ////// private ResourceManager stringManager = Init.StringManager;

        /// <summary>
        /// The transaction log manager
        /// </summary>
        private ITransactionManager transactionLogManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionResponseService"/> class.
        /// </summary>
        public TransactionResponseService()
        {
            this.transactionLogManager = StaticPersistence.TransactionLogManager;
        }

        /// <summary>
        /// Web service contract for receiving transaction responses from ODM.
        /// </summary>
        /// <param name="request">The request.</param>
        public void TransactionResponse(Message request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            using (XmlReader reader = request.GetReaderAtBodyContents())
            {
                var serializer = new XmlSerializer(typeof(TransactionResponseServiceRequest));
                TransactionResponseServiceRequest transactionResponse = (TransactionResponseServiceRequest)serializer.Deserialize(reader);

                this.transactionLogManager.UpdateTransactionStatus(transactionResponse.listenerTransactionId, (int)transactionResponse.status, transactionResponse.message, transactionResponse.debugInfo);
            }

            return;
        }

        /*
         * ODM web service mocks
         *
        /// <summary>
        /// Update the asset.
        /// </summary>
        /// <param name="AssetUpdateServiceRequest">The service request to update the asset.</param>
        public void AssetUpdate(Message AssetUpdateServiceRequest)
        {
            return;
        }

        /// <summary>
        /// Load the asset.
        /// </summary>
        /// <param name="AssetLoadServiceRequest">The service request to load the asset.</param>
        public void AssetLoad(Message AssetLoadServiceRequest)
        {
            return;
        }

        /// <summary>
        /// The test result.
        /// </summary>
        /// <param name="TestResultServiceRequest">The service request to save test result.</param>
        public void TestResult(Message TestResultServiceRequest)
        {
            return;
        }
        */
    }
}
