namespace AMSLLC.Listener.Domain.WNP.Unit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using WorkstationAggregate;

    /// <summary>
    /// Summary description for WorkstationTests
    /// </summary>
    [TestClass]
    public class WorkstionTests
    {
        #region Box Number Test 

        #region Negative Tests

        [TestMethod]
        public void BoxNumberActionRequiredButValueIsNull()
        {          
            string actionBox = "R";
            string boxNumber = null;            
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber, expectedMessage);
        }

        [TestMethod]
        public void BoxNumberActionRequiredButValueIsEmpty()
        {
            string actionBox = "R";
            string boxNumber = string.Empty;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber, expectedMessage);
        }

        [TestMethod]
        public void BoxNumberActionRequiredButValueIsWhitespace()
        {
            string actionBox = "R";
            string boxNumber = "   ";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber, expectedMessage);
        }

        [TestMethod]
        public void BoxNumberActionClearedButValueIsProvided()
        {           
            var actionBox = "C";
            string boxNumber = "123";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber, expectedMessage);           
        }      

        [TestMethod]
        public void BoxNumberActionDisabledButValueIsProvided()
        {
            var actionBox = "D";
            string boxNumber = "123";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled";
            ExecuteBoxNumberNegativeTest(actionBox, boxNumber, expectedMessage);
        }       

        #endregion

        #region Positive Tests       
        [TestMethod]
        public void BoxNumberActionNoneAndValueIsNull()
        {
            string actionBox = null;
            string boxNumber = null;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionNoneAndValueIsEmpty()
        {
            string actionBox = string.Empty;
            string boxNumber = string.Empty;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionNoneAndValueIsWhiteSpace()
        {
            string actionBox = " ";
            string boxNumber = " ";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionDisabledAndValueIsNull()
        {
            string actionBox = "D";
            string boxNumber = null;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionDisabledAndValueIsEmpty()
        {
            string actionBox = "D";
            string boxNumber = string.Empty;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionDisabledAndValueIsWhiteSpace()
        {
            string actionBox = "D";
            string boxNumber = " ";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsProvided()
        {
            string actionBox = "E";
            string boxNumber = "123";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsNull()
        {
            string actionBox = "E";
            string boxNumber = null;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsEmpty()
        {
            string actionBox = "E";
            string boxNumber = string.Empty;
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionEnabledAndValueIsWhitespace()
        {
            string actionBox = "E";
            string boxNumber = " ";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        [TestMethod]
        public void BoxNumberActionRequiredAndValueIsProvided()
        {
            string actionBox = "R";
            string boxNumber = "123";
            ExecuteBoxNumberPositiveTest(actionBox, boxNumber);
        }

        #endregion

        #endregion

        #region Supporting Functions for tests

        private void ExecuteBoxNumberPositiveTest(string actionBox, string boxNumber)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, actionBox, null, null, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", boxNumber, "", null, null, null, GetLocation(location));
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        private void ExecuteBoxNumberNegativeTest(string actionBox, string boxNumber, string expectedMessage)
        {
            string actionName = "Install Meter";  
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, actionBox, null, null, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", boxNumber, null, null, null, null, GetLocation(location));
                Assert.Fail();
            }
            catch (Exception ex)
            {
               // string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'");
                Assert.IsTrue(ex is ArgumentException);                
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(boxNumber)).ToLower()));
            }
        }

        private Location GetLocation(IMemento locationMemento)
        {
            var location = new Location();
            ((IOriginator)location).SetMemento(locationMemento);
            return location;
        }

        private IMemento GetLocationMemento()
        {
            return new LocationMemento("BETHLEHEM", "A");
        }

        private IMemento GetWorkstationMemento(List<IMemento> businessActions, IMemento location)
        {
            return new WorkstationMemento("CSS", businessActions, GetIncomingRules(location));
        }

        private IMemento GetEquipmentMemento(IMemento location)
        {
            return new EquipmentStateMemento("A001003111", "EM", "Test Program", location, "V", "V", 0, null, null, null, null, null);
        }

        private EquipmentState GetEquipment(IMemento location)
        {
            var equipment = new EquipmentState();
            ((IOriginator)equipment).SetMemento(GetEquipmentMemento(location));
            return equipment;
        }

        private Workstation GetWorkstation(IMemento businessAction, IMemento location)
        {
            var memento = GetWorkstationMemento(new List<IMemento> { businessAction }, location);
            var workstation = new Workstation();
            ((IOriginator)workstation).SetMemento(memento);
            return workstation;
        }

        private IEnumerable<IncomingRuleMemento> GetIncomingRules(IMemento location)
        {
            return new List<IncomingRuleMemento> { new IncomingRuleMemento("Test Program", true, "V", "V", location, "A", "rule message") };
        }

        #endregion
    }
}
