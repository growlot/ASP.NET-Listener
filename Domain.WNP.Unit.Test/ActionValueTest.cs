namespace AMSLLC.Listener.Domain.WNP.Unit.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Summary description for ActionValueTest
    /// </summary>
    [TestClass]
    public class ActionValueTest
    {


        [TestMethod]
        public void CreateActionValueFromNull()
        {
            var actionValue = new ActionValue(null);
            Assert.AreEqual("D", actionValue.Code);
        }

        [TestMethod]
        public void EmptyStringValueTest()
        {
            var actionValue = new ActionValue(string.Empty);
            Assert.AreEqual(ActionValue.Disabled, actionValue);
        }

        [TestMethod]
        public void CaseSensitiveActionValueCreation()
        {
            try
            {
                var actionValue = new ActionValue("r");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void OutOfRangeValueTest()
        {
            try
            {
                var actionValue = new ActionValue("test");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentOutOfRangeException));
            }
        }

        [TestMethod]
        public void OutOfRangeMessageTest()
        {
            var supportedActions = new List<string>(new[] { "D", "E", "R", "C" });
            try
            {
                var actionValue = new ActionValue("dummy");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(ArgumentOutOfRangeException));
                var expectedMessage = "Action value is not recognized. Supported values are: {0}".FormatWith(string.Join(",", supportedActions.ToArray()));
                Assert.IsTrue(ex.Message.Contains(expectedMessage));
            }
        }

        [TestMethod]
        public void CompareValueTest()
        {
            var actionValue = new ActionValue("R");
            Assert.AreEqual(ActionValue.Required, actionValue);
        }

        [TestMethod]
        public void DisabledActionValueTest()
        {
            var actionValue = new ActionValue("D");
            Assert.AreEqual(ActionValue.Disabled, actionValue);
        }

        [TestMethod]
        public void EnabledActionValueTest()
        {
            var actionValue = new ActionValue("E");
            Assert.AreEqual(ActionValue.Enabled, actionValue);
        }

        [TestMethod]
        public void ClearActionValueTest()
        {
            var actionValue = new ActionValue("C");
            Assert.AreEqual(ActionValue.Clear, actionValue);
        }
    }
}
