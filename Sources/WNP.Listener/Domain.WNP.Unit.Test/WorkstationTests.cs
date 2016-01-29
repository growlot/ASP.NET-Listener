// <copyright file="WorkstationTests.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.Domain.WNP.Unit.Test
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WorkstationAggregate;

    /// <summary>
    /// Test class for business actions rules in workstation
    /// </summary>
    [TestClass]
    public class WorkstationTests
    {
        /// <summary>
        /// Test boxNumber parameter when ActionBox is required and value is null
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoxNumberActionRequiredButValueIsNull()
        {
            string actionBox = "R";
            string boxNumber = null;
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number parameter when action box is clear and value is provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoxNumberActionClearedButValueIsProvided()
        {
            var actionBox = "C";
            string boxNumber = "123";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number parameter when action box is disabled and value is provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void BoxNumberActionDisabledButValueIsProvided()
        {
            var actionBox = "D";
            string boxNumber = "123";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number paramter when action box is disbaled and value is null
        /// </summary>
        [TestMethod]
        public void BoxNumberActionDisabledAndValueIsNull()
        {
            string actionBox = "D";
            string boxNumber = null;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number parameter when action box is enabled and value is valid
        /// </summary>
        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsProvided()
        {
            string actionBox = "E";
            string boxNumber = "123";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number parametr when action box is enabled and vlaue is null
        /// </summary>
        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsNull()
        {
            string actionBox = "E";
            string boxNumber = null;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        /// <summary>
        /// Test box number parameter when action box is required and value is provided
        /// </summary>
        [TestMethod]
        public void BoxNumberActionRequiredAndValueIsProvided()
        {
            string actionBox = "R";
            string boxNumber = "123";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        private static Location GetLocation(IMemento locationMemento)
        {
            var location = new Location();
            ((IOriginator)location).SetMemento(locationMemento);
            return location;
        }

        private static IMemento GetLocationMemento()
        {
            return new LocationMemento("BETHLEHEM", "A");
        }

        private static IMemento GetEquipmentMemento(IMemento location)
        {
            return new EquipmentStateMemento("A001003111", "EM", "Test Program", location, "V", "V", 0, null, null, null, null, null);
        }

        private static IEnumerable<IncomingRuleMemento> GetIncomingRules(IMemento location)
        {
            var message = Properties.Resources.BusinessRuleMessage;
            return new List<IncomingRuleMemento> { new IncomingRuleMemento("Test Program", true, "V", "V", location, "A", message) };
        }

        private static IMemento GetWorkstationMemento(List<IMemento> businessActions, IMemento location)
        {
            return new WorkstationMemento("CSS", businessActions, GetIncomingRules(location));
        }

        private static EquipmentState GetEquipment(IMemento location)
        {
            var equipment = new EquipmentState();
            ((IOriginator)equipment).SetMemento(GetEquipmentMemento(location));
            return equipment;
        }

        private static Workstation GetWorkstation(IMemento businessAction, IMemento location)
        {
            var memento = GetWorkstationMemento(new List<IMemento> { businessAction }, location);
            var workstation = new Workstation();
            ((IOriginator)workstation).SetMemento(memento);
            return workstation;
        }

        private static void ExecuteBoxNumberPositiveTest(string actionBox, string boxNumber)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, actionBox, "D", "D", "D", "D");
            var workstation = GetWorkstation(businessAction, location);
            workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", boxNumber, string.Empty, null, null, null, GetLocation(location));
        }

        private static void ExecuteBoxNumberNegativeTest(string actionBox, string boxNumber)
        {
            string actionName = "Install Meter";
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, actionBox, "D", "D", "D", "D");
            var workstation = GetWorkstation(businessAction, location);
            workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", boxNumber, null, null, null, null, GetLocation(location));
        }
    }
}
