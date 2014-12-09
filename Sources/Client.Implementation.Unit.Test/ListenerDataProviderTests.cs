//-----------------------------------------------------------------------
// <copyright file="ListenerDataProviderTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace Client.Implementation.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using AMSLLC.Listener.Client.Implementation;
    using AMSLLC.Listener.Client.Implementation.Messages;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate.Criterion;

    /// <summary>
    /// Tests Listener data provider
    /// </summary>
    [TestClass]
    public class ListenerDataProviderTests
    {
        /// <summary>
        /// Get transaction log results should be empty if equipment type is not supported.
        /// </summary>
        [TestMethod]
        public void GetTransactionLogResultsShouldBeEmptyIfEquipmentTypeIsNotSupported()
        {
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

                ListenerDataProvider dataProider = new ListenerDataProvider(persistenceManager);
                response = dataProider.GetTransactionLog(request);
            }

            Assert.AreEqual(response.Count, 0);
        }

        /// <summary>
        /// Get transaction log results should be empty if device does not exist.
        /// </summary>
        [TestMethod]
        public void GetTransactionLogResultsShouldBeEmptyIfDeviceDoesNotExist()
        {
            IList<TransactionLogResponse> response;
            TransactionLogRequest request = new TransactionLogRequest()
            {
                ServiceType = "E",
                EquipmentType = "EM",
                EquipmentNumber = "123"
            };

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<EquipmentType>((criteria) =>
                {
                    EquipmentType equipmentType = new EquipmentType()
                    {
                        Id = 1,
                        Description = "Electric Meter",
                        InternalCode = "EM",
                        ServiceType = new ServiceType()
                        {
                            Id = 1,
                            Description = "Electric",
                            InternalCode = "E"                            
                        }
                    };

                    return equipmentType;
                });
                persistenceManager.RetrieveAllEqualOf1DetachedCriteria<Device>((criteria) =>
                {
                    return null;
                });

                ListenerDataProvider dataProider = new ListenerDataProvider(persistenceManager);
                response = dataProider.GetTransactionLog(request);
            }

            Assert.AreEqual(response.Count, 0);
        }
    }
}
