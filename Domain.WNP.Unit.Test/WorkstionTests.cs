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
        [TestMethod]
        public void BoxNumberRequiredValueTest()
        {
            IMemento newLocation = new LocationMemento("BETHLEHEM", "A");
            IMemento bAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", newLocation, "S", false, "R", null, null, null, null);
            IMemento incomingRule = new IncomingRuleMemento("Test Program", true, "V", "V", newLocation, "A", "rule message");
            IMemento wsMemento = new WorkstationMemento("CSS", new List<IMemento> { bAction }, new List<IMemento> { incomingRule });
            IMemento equipmentMemento = new EquipmentStateMemento("A001003111", "EM", "Test Program", newLocation, "V", "V", 0, null, null, null, null, null);

            var location = new Location();
            ((IOriginator)location).SetMemento(newLocation);

            var equipment = new EquipmentState();
            ((IOriginator)equipment).SetMemento(equipmentMemento);

            var workstation = new Workstation();
            ((IOriginator)workstation).SetMemento(wsMemento);

            try
            {
                workstation.PerformBusinessAction(equipment, "Install Meter", null, null, null, null, null, location);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                {
                    Assert.Fail();
                }

                var expected = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'".FormatWith("Install Meter", workstation.Id, "boxNumber");
                Assert.AreEqual(expected, ex.Message);
            }
        }

        [TestMethod]
        public void ClearBoxNumberActionTest()
        {
            IMemento newLocation = new LocationMemento("BETHLEHEM", "A");
            IMemento bAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", newLocation, "S", false, "C", null, null, null, null);
            IMemento incomingRule = new IncomingRuleMemento("Test Program", true, "V", "V", newLocation, "A", "rule message");
            IMemento wsMemento = new WorkstationMemento("CSS", new List<IMemento> { bAction }, new List<IMemento> { incomingRule });
            IMemento equipmentMemento = new EquipmentStateMemento("A001003111", "EM", "Test Program", newLocation, "V", "V", 0, "123456", null, null, null, null);

            var location = new Location();
            ((IOriginator)location).SetMemento(newLocation);

            var equipment = new EquipmentState();
            ((IOriginator)equipment).SetMemento(equipmentMemento);

            var workstation = new Workstation();
            ((IOriginator)workstation).SetMemento(wsMemento);

            try
            {
                workstation.PerformBusinessAction(equipment, "Install Meter", "boxnumber", null, null, null, null, location);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                if (!(ex is ArgumentException))
                {
                    Assert.Fail();
                }
                
                var expected = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear".FormatWith("Install Meter", workstation.Id, "boxNumber");
                Assert.AreEqual(expected, ex.Message);
            }
        }
    }
}
