//-----------------------------------------------------------------------
// <copyright file="ListenerDataProviderTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Client.Implementation.Unit.Test
{
    using System.Collections.Generic;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests Listener data provider
    /// </summary>
    [TestClass]
    public class ListenerDataProviderTests
    {
        /// <summary>
        /// Get transaction log results should not perform search if equipment type is not supported.
        /// It should return empty list.
        /// </summary>
        [TestMethod]
        public void GetTransactionLogResultsShouldNotPerformSearchIfEquipmentTypeIsNotSupported()
        {
            bool searchPerformed = false;
            IList<TransactionLogResponse> response;
            TransactionLogRequest request = new TransactionLogRequest()
            {
                ServiceType = "E",
                EquipmentType = "ZZ",
                EquipmentNumber = "123"
            };

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<EquipmentType>((criteria) =>
                {
                    return null;
                });

                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<TransactionLog>((criteria) =>
                {
                    searchPerformed = true;
                    return new List<TransactionLog>();
                });

                ListenerDataProvider dataProider = new ListenerDataProvider(persistenceManager);
                response = dataProider.GetTransactionLog(request);
            }

            Assert.AreEqual(response.Count, 0, "Response should contain empty list");
            Assert.AreEqual(false, searchPerformed);
        }

        /// <summary>
        /// Get transaction log results should not perform search if company does not exist.
        /// It should return empty list.
        /// </summary>
        [TestMethod]
        public void GetTransactionLogResultsShouldNotPerformSearchIfCompanyDoesNotExist()
        {
            bool searchPerformed = false;
            IList<TransactionLogResponse> response;
            TransactionLogRequest request = new TransactionLogRequest()
            {
                CompanyId = 0,
                ServiceType = "E",
                EquipmentType = "EM",
            };

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<EquipmentType>((criteria) =>
                {
                    return new EquipmentType(1);
                });
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Company>((criteria) =>
                {
                    return null;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<TransactionLog>((criteria) =>
                {
                    searchPerformed = true;
                    return new List<TransactionLog>();
                });

                ListenerDataProvider dataProider = new ListenerDataProvider(persistenceManager);
                response = dataProider.GetTransactionLog(request);
            }

            Assert.AreEqual(response.Count, 0, "Response should contain empty list");
            Assert.AreEqual(false, searchPerformed);
        }

        /// <summary>
        /// Get transaction log results should not perform search if device does not exist.
        /// It should return empty list.
        /// </summary>
        [TestMethod]
        public void GetTransactionLogResultsShouldNotPerformSearchIfDeviceDoesNotExist()
        {
            bool searchPerformed = false;
            bool triedToRetrieveDevice = false;

            IList<TransactionLogResponse> response;
            TransactionLogRequest request = new TransactionLogRequest()
            {
                CompanyId = 0,
                ServiceType = "E",
                EquipmentType = "EM",
                EquipmentNumber = "123"
            };

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<EquipmentType>((criteria) =>
                {
                    return new EquipmentType(1);
                });
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Company>((criteria) =>
                {
                    return new Company(1);
                });
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Device>((criteria) =>
                {
                    triedToRetrieveDevice = true;
                    return null;
                });

                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<TransactionLog>((criteria) =>
                {
                    searchPerformed = true;
                    return new List<TransactionLog>();
                });

                ListenerDataProvider dataProider = new ListenerDataProvider(persistenceManager);
                response = dataProider.GetTransactionLog(request);
            }

            Assert.AreEqual(response.Count, 0, "Response should contain empty list");
            Assert.AreEqual(true, triedToRetrieveDevice, "Device retrieval should be called");
            Assert.AreEqual(false, searchPerformed, "Search should not be performed");
        }
    }
}
