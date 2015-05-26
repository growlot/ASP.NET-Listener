//-----------------------------------------------------------------------
// <copyright file="WNPSystemTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Common.WNP.Unit.Test
{
    using System;
    using AMSLLC.Listener.Common.WNP.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for WNPSystem class
    /// </summary>
    [TestClass]
    public class WNPSystemTests
    {
        /// <summary>
        /// The create date for meter must be preserved.
        /// </summary>
        [TestMethod]
        public void MeterCreateDateMustBePreserved()
        {
            Meter motherMeter = MotherObjects.DefaultMeter();
            bool saveInvoked = false;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Meter>((criteria) =>
                {
                    return motherMeter;
                });

                persistenceManager.SaveOf1M0<Meter>(item =>
                {
                    saveInvoked = true;
                    Assert.AreEqual(item.CreateDate, motherMeter.CreateDate);
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                Meter meter = new Meter()
                {
                    EquipmentNumber = motherMeter.EquipmentNumber,
                    Owner = MotherObjects.DefaultOwner()
                };
                wnpSystem.AddOrReplaceEquipment<Meter>(meter);
            }

            Assert.IsTrue(saveInvoked);
        }

        /// <summary>
        /// The create date should be set to now during meter record creation.
        /// </summary>
        [TestMethod]
        public void MeterCreateDateShouldBeSetToNowIfNullDuringCreation()
        {
            Meter motherMeter = MotherObjects.DefaultMeter();
            bool saveInvoked = false;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.SaveOf1M0<Meter>(item =>
                {
                    saveInvoked = true;
                    Assert.AreEqual(item.CreateDate.Date, DateTime.Now.Date);
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                motherMeter.CreateDate = default(DateTime);
                wnpSystem.AddOrReplaceEquipment<Meter>(motherMeter);
            }

            Assert.IsTrue(saveInvoked);
        }

        /// <summary>
        /// The create date should be set to now if it was empty during meter record update.
        /// </summary>
        [TestMethod]
        [TestCategory("LIS-255")]
        public void MeterCreateDateShouldBeSetToNowIfNullDuringUpdate()
        {
            Meter motherMeter = MotherObjects.DefaultMeter();
            
            // original record has null create date
            motherMeter.CreateDate = default(DateTime);

            bool saveInvoked = false;

            // Create the fake persistenceManager
            using (var persistenceManager = new AMSLLC.Listener.Common.Fakes.StubIPersistenceManager())
            {
                persistenceManager.RetrieveFirstEqualOf1DetachedCriteria<Meter>((criteria) =>
                {
                    return motherMeter;
                });

                persistenceManager.SaveOf1M0<Meter>(item =>
                {
                    saveInvoked = true;
                    Assert.AreEqual(item.CreateDate.Date, DateTime.Now.Date);
                });

                IWNPPersistenceController persistenceController = new WNPPersistenceController();
                persistenceController.InitializeListenerSystems(persistenceManager);
                persistenceController.InitializeListenerClientSystems(persistenceManager);
                WNPSystem wnpSystem = persistenceController.WNPSystem;

                Meter meter = new Meter()
                {
                    EquipmentNumber = motherMeter.EquipmentNumber,
                    Owner = MotherObjects.DefaultOwner()
                };
                wnpSystem.AddOrReplaceEquipment<Meter>(meter);
            }

            Assert.IsTrue(saveInvoked);
        }
    }
}