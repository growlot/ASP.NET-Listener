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

        #region Pallet Number Test 

        #region Negative Tests

        [TestMethod]
        public void PalletNumberActionRequiredButValueIsNull()
        {
            string actionPallet = "R";
            string palletNumber = null;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecutePalletNumberNegativeTest(actionPallet, palletNumber, expectedMessage);
        }

        [TestMethod]
        public void PalletNumberActionRequiredButValueIsEmpty()
        {
            string actionPallet = "R";
            string palletNumber = string.Empty;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecutePalletNumberNegativeTest(actionPallet, palletNumber, expectedMessage);
        }

        [TestMethod]
        public void PalletNumberActionRequiredButValueIsWhitespace()
        {
            string actionPallet = "R";
            string palletNumber = "   ";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecutePalletNumberNegativeTest(actionPallet, palletNumber, expectedMessage);
        }

        [TestMethod]
        public void PalletNumberActionClearedButValueIsProvided()
        {
            string actionPallet = "C";
            string palletNumber = "123123";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear";
            ExecutePalletNumberNegativeTest(actionPallet, palletNumber, expectedMessage);
        }

        [TestMethod]
        public void PalletNumberActionDisabledButValueIsProvided()
        {
            string actionPallet = "D";
            string palletNumber = "123123";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled";
            ExecutePalletNumberNegativeTest(actionPallet, palletNumber, expectedMessage);
        }

        #endregion

        #region Positive Tests       
        [TestMethod]
        public void PalletNumberActionNoneAndValueIsNull()
        {
            string actionPallet = null;
            string palletNumber = null;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionNoneAndValueIsEmpty()
        {
            string actionPallet = string.Empty;
            string palletNumber = string.Empty;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionNoneAndValueIsWhiteSpace()
        {
            string actionPallet = " ";
            string palletNumber = " ";
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionDisabledAndValueIsNull()
        {
            string actionPallet = "D";
            string palletNumber = null;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionDisabledAndValueIsEmpty()
        {
            string actionPallet = "D";
            string palletNumber = string.Empty;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionDisabledAndValueIsWhiteSpace()
        {
            string actionPallet = "D";
            string palletNumber = "  ";
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionEnabledAndValueIsProvided()
        {
            string actionPallet = "E";
            string palletNumber = "56465  ";
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionEnabledAndValueIsNull()
        {
            string actionPallet = "E";
            string palletNumber = null;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionEnabledAndValueIsEmpty()
        {
            string actionPallet = "E";
            string palletNumber = string.Empty;
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionEnabledAndValueIsWhitespace()
        {
            string actionPallet = "E";
            string palletNumber = "   ";
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        [TestMethod]
        public void PalletNumberActionRequiredAndValueIsProvided()
        {
            string actionPallet = "R";
            string palletNumber = "465465";
            ExecutePalletNumberPositiveTest(actionPallet, palletNumber);
        }

        #endregion

        #endregion

        #region Shelf Id Tests 

        #region Negative Tests

        [TestMethod]
        public void ShelfIdActionRequiredButValueIsNull()
        {
            string actionShelf = "R";
            string shelfId = null;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteShelfIdNegativeTest(actionShelf, shelfId, expectedMessage);
        }

        [TestMethod]
        public void ShelfIdActionRequiredButValueIsEmpty()
        {
            string actionShelf = "R";
            string shelfId = string.Empty;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteShelfIdNegativeTest(actionShelf, shelfId, expectedMessage);
        }

        [TestMethod]
        public void ShelfIdActionRequiredButValueIsWhitespace()
        {
            string actionShelf = "R";
            string shelfId = "  ";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteShelfIdNegativeTest(actionShelf, shelfId, expectedMessage);
        }

        [TestMethod]
        public void ShelfIdActionClearedButValueIsProvided()
        {
            string actionShelf = "C";
            string shelfId = "123";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear";
            ExecuteShelfIdNegativeTest(actionShelf, shelfId, expectedMessage);
        }

        [TestMethod]
        public void ShelfIdActionDisabledButValueIsProvided()
        {
            string actionShelf = "D";
            string shelfId = "34342";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled";
            ExecuteShelfIdNegativeTest(actionShelf, shelfId, expectedMessage);
        }

        #endregion

        #region Positive Tests       
        [TestMethod]
        public void ShelfIdActionNoneAndValueIsNull()
        {
            string actionShelf = null;
            string shelfId = null;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionNoneAndValueIsEmpty()
        {
            string actionShelf = string.Empty;
            string shelfId = string.Empty;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionNoneAndValueIsWhiteSpace()
        {
            string actionShelf = "  ";
            string shelfId = "  ";
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionDisabledAndValueIsNull()
        {
            string actionShelf = "D";
            string shelfId = null;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionDisabledAndValueIsEmpty()
        {
            string actionShelf = "D";
            string shelfId = string.Empty;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionDisabledAndValueIsWhiteSpace()
        {
            string actionShelf = "D";
            string shelfId = "    ";
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionEnabledAndValueIsProvided()
        {
            string actionShelf = "E";
            string shelfId = "3434343";
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionEnabledAndValueIsNull()
        {
            string actionShelf = "E";
            string shelfId = null;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionEnabledAndValueIsEmpty()
        {
            string actionShelf = "E";
            string shelfId = string.Empty;
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionEnabledAndValueIsWhitespace()
        {
            string actionShelf = "E";
            string shelfId = "   ";
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        [TestMethod]
        public void ShelfIdActionRequiredAndValueIsProvided()
        {
            string actionShelf = "E";
            string shelfId = "3434434";
            ExecuteShelfIdPositiveTest(actionShelf, shelfId);
        }

        #endregion

        #endregion

        #region Issued To Tests 

        #region Negative Tests

        [TestMethod]
        public void IssuedToActionRequiredButValueIsNull()
        {
            string actionReceivedBy = "R";
            string issuedTo = null;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteIssuedToNegativeTest(actionReceivedBy, issuedTo, expectedMessage);
        }

        [TestMethod]
        public void IssuedToActionRequiredButValueIsEmpty()
        {
            string actionReceivedBy = "R";
            string issuedTo = string.Empty;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteIssuedToNegativeTest(actionReceivedBy, issuedTo, expectedMessage);
        }

        [TestMethod]
        public void IssuedToActionRequiredButValueIsWhitespace()
        {
            string actionReceivedBy = "R";
            string issuedTo = "    ";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteIssuedToNegativeTest(actionReceivedBy, issuedTo, expectedMessage);
        }

        [TestMethod]
        public void IssuedToActionClearedButValueIsProvided()
        {
            string actionReceivedBy = "C";
            string issuedTo = "TEST USER";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear";
            ExecuteIssuedToNegativeTest(actionReceivedBy, issuedTo, expectedMessage);
        }

        [TestMethod]
        public void IssuedToActionDisabledButValueIsProvided()
        {
            string actionReceivedBy = "D";
            string issuedTo = "another user";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled";
            ExecuteIssuedToNegativeTest(actionReceivedBy, issuedTo, expectedMessage);
        }

        #endregion

        #region Positive Tests       
        [TestMethod]
        public void IssuedToActionNoneAndValueIsNull()
        {
            string actionReceivedBy = null;
            string issuedTo = null;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionNoneAndValueIsEmpty()
        {
            string actionReceivedBy = string.Empty;
            string issuedTo = string.Empty;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionNoneAndValueIsWhiteSpace()
        {
            string actionReceivedBy = "   ";
            string issuedTo = "  ";
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionDisabledAndValueIsNull()
        {
            string actionReceivedBy = "D";
            string issuedTo = null;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionDisabledAndValueIsEmpty()
        {
            string actionReceivedBy = "D";
            string issuedTo = string.Empty;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionDisabledAndValueIsWhiteSpace()
        {
            string actionReceivedBy = "D";
            string issuedTo = "   ";
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionEnabledAndValueIsProvided()
        {
            string actionReceivedBy = "E";
            string issuedTo = "2ND USER";
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionEnabledAndValueIsNull()
        {
            string actionReceivedBy = "E";
            string issuedTo = null;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionEnabledAndValueIsEmpty()
        {
            string actionReceivedBy = "E";
            string issuedTo = string.Empty;
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionEnabledAndValueIsWhitespace()
        {
            string actionReceivedBy = "E";
            string issuedTo = "   ";
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        [TestMethod]
        public void IssuedToActionRequiredAndValueIsProvided()
        {
            string actionReceivedBy = "E";
            string issuedTo = "TEMP USER";
            ExecuteIssuedToPositiveTest(actionReceivedBy, issuedTo);
        }

        #endregion

        #endregion

        #region Vehicle Number Tests 

        #region Negative Tests

        [TestMethod]
        public void VehicleNumberActionRequiredButValueIsNull()
        {
            string actionVehicleNumber = "R";
            string vehicleId = null;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteVehicleNumberNegativeTest(actionVehicleNumber, vehicleId, expectedMessage);
        }

        [TestMethod]
        public void VehicleNumberActionRequiredButValueIsEmpty()
        {
            string actionVehicleNumber = "R";
            string vehicleId = string.Empty;
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteVehicleNumberNegativeTest(actionVehicleNumber, vehicleId, expectedMessage);
        }

        [TestMethod]
        public void VehicleNumberActionRequiredButValueIsWhitespace()
        {
            string actionVehicleNumber = "R";
            string vehicleId = "    ";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value not provided for required parameter '{2}'";
            ExecuteVehicleNumberNegativeTest(actionVehicleNumber, vehicleId, expectedMessage);
        }

        [TestMethod]
        public void VehicleNumberActionClearedButValueIsProvided()
        {
            string actionVehicleNumber = "C";
            string vehicleId = "TEST USER";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to clear";
            ExecuteVehicleNumberNegativeTest(actionVehicleNumber, vehicleId, expectedMessage);
        }

        [TestMethod]
        public void VehicleNumberActionDisabledButValueIsProvided()
        {
            string actionVehicleNumber = "D";
            string vehicleId = "another user";
            string expectedMessage = "Can not perform business action '{0}' for workstation '{1}' becasue value is provided for parameter '{2}' where action field is set to disabled";
            ExecuteVehicleNumberNegativeTest(actionVehicleNumber, vehicleId, expectedMessage);
        }

        #endregion

        #region Positive Tests       
        [TestMethod]
        public void VehicleNumberActionNoneAndValueIsNull()
        {
            string actionVehicleNumber = null;
            string vehicleId = null;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionNoneAndValueIsEmpty()
        {
            string actionVehicleNumber = string.Empty;
            string vehicleId = string.Empty;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionNoneAndValueIsWhiteSpace()
        {
            string actionVehicleNumber = "   ";
            string vehicleId = "  ";
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionDisabledAndValueIsNull()
        {
            string actionVehicleNumber = "D";
            string vehicleId = null;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionDisabledAndValueIsEmpty()
        {
            string actionVehicleNumber = "D";
            string vehicleId = string.Empty;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionDisabledAndValueIsWhiteSpace()
        {
            string actionVehicleNumber = "D";
            string vehicleId = "   ";
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionEnabledAndValueIsProvided()
        {
            string actionVehicleNumber = "E";
            string vehicleId = "2ND USER";
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionEnabledAndValueIsNull()
        {
            string actionVehicleNumber = "E";
            string vehicleId = null;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionEnabledAndValueIsEmpty()
        {
            string actionVehicleNumber = "E";
            string vehicleId = string.Empty;
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionEnabledAndValueIsWhitespace()
        {
            string actionVehicleNumber = "E";
            string vehicleId = "   ";
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        [TestMethod]
        public void VehicleNumberActionRequiredAndValueIsProvided()
        {
            string actionVehicleNumber = "E";
            string vehicleId = "TEMP USER";
            ExecuteVehicleNumberPositiveTest(actionVehicleNumber, vehicleId);
        }

        #endregion

        #endregion

        #region Generic Test Functions

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
                Assert.IsTrue(ex is ArgumentException);
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(boxNumber)).ToLower()));
            }
        }

        private void ExecutePalletNumberPositiveTest(string actionPallet, string palletNumber)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, null, actionPallet, null, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, palletNumber, null, null, null, GetLocation(location));
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        private void ExecutePalletNumberNegativeTest(string actionPallet, string palletNumber, string expectedMessage)
        {
            string actionName = "Install Meter";
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, null, actionPallet, null, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, palletNumber, null, null, null, GetLocation(location));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(palletNumber)).ToLower()));
            }
        }

        private void ExecuteShelfIdPositiveTest(string actionShelf, string shelfId)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, null, null, actionShelf, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, shelfId, null, null, GetLocation(location));
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        private void ExecuteShelfIdNegativeTest(string actionShelf, string shelfId, string expectedMessage)
        {
            string actionName = "Install Meter";
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, null, null, actionShelf, null, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, shelfId, null, null, GetLocation(location));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(shelfId)).ToLower()));
            }
        }

        private void ExecuteIssuedToPositiveTest(string actionReceivedBy, string issuedTo)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, null, null, null, actionReceivedBy, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, null, issuedTo, null, GetLocation(location));
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        private void ExecuteIssuedToNegativeTest(string actionReceivedBy, string issuedTo, string expectedMessage)
        {
            string actionName = "Install Meter";
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, null, null, null, actionReceivedBy, null);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, null, issuedTo, null, GetLocation(location));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(issuedTo)).ToLower()));
            }
        }

        private void ExecuteVehicleNumberPositiveTest(string actionVehicleNumber, string vehicleId)
        {
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento("Install Meter", "Test Program", "Test Program", "V", "V", location, "S", false, null, null, null, null, actionVehicleNumber);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, null, null, vehicleId, GetLocation(location));
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.Fail();
            }
        }

        private void ExecuteVehicleNumberNegativeTest(string actionVehicleNumber, string vehicleId, string expectedMessage)
        {
            string actionName = "Install Meter";
            var location = GetLocationMemento();
            var businessAction = new BusinessActionMemento(actionName, "Test Program", "Test Program", "V", "V", location, "S", false, null, null, null, null, actionVehicleNumber);
            var workstation = GetWorkstation(businessAction, location);

            try
            {
                workstation.PerformBusinessAction(GetEquipment(location), "Install Meter", null, null, null, null, vehicleId, GetLocation(location));
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentException);
                Assert.IsTrue(ex.Message.ToLower().Contains(expectedMessage.FormatWith(actionName, workstation.Id, nameof(vehicleId)).ToLower()));
            }
        }

        #endregion

        #region Supporting Functions for tests

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
