//-----------------------------------------------------------------------
// <copyright file="TransactionHelper.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP.Lookup;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
    using log4net;

    /// <summary>
    /// Static helper methods for transaction handling.
    /// </summary>
    public static class TransactionHelper
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The transaction manager
        /// </summary>
        private static ITransactionManager transactionManager = StaticPersistence.TransactionLogManager;

        /// <summary>
        /// Processes the meters batch.
        /// </summary>
        /// <param name="batch">The batch.</param>
        /// <param name="deviceBatch">The device batch.</param>
        /// <param name="company">The company.</param>
        /// <param name="processMeter">The method that will process meter transaction.</param>
        /// <param name="processMeterTestResults">The method that will process meter test results transaction.</param>
        /// <exception cref="System.ArgumentNullException">
        /// processMeter;There are transactions defined for the meter record, but it is not specified how to process them.
        /// or
        /// processMeterTestResults;There are transactions defined for the meter test record, but it is not specified how to process them.
        /// </exception>
        /// <exception cref="System.AggregateException">All errors from sub transactions.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "4", Justification = "False positive. Argument is validated")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "3", Justification = "False positive. Argument is validated")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "All exceptions are logged and processing is continued.")]
        public static void ProcessMetersBatch(NewBatch batch, DeviceBatch deviceBatch, Company company, Action<Device, Meter, int, bool> processMeter, Action<Device, DeviceTest, Meter, int> processMeterTestResults)
        {
            string errorMessage = string.Empty;
            IList<Meter> batchMeters = StaticPersistence.WnpSystem.GetEquipmentByBatch<Meter>(batch);

            IList<TransactionType> deviceTransactionTypes = transactionManager.GetTransactionTypes(TransactionDataLookup.Device, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);
            if (deviceTransactionTypes.Count > 0 && processMeter == null)
            {
                throw new ArgumentNullException("processMeter", "There are transactions defined for the meter record, but it is not specified how to process them.");
            }

            IList<TransactionType> deviceTestTransactionTypes = transactionManager.GetTransactionTypes(TransactionDataLookup.DeviceTest, TransactionDirectionLookup.Outgoing, TransactionSourceLookup.WNP);
            if (deviceTestTransactionTypes.Count > 0 && processMeterTestResults == null)
            {
                throw new ArgumentNullException("processMeterTestResults", "There are transactions defined for the meter test record, but it is not specified how to process them.");
            }

            foreach (Meter meter in batchMeters)
            {
                Device device = CreateDevice(meter, company);

                if (deviceTransactionTypes.Count > 0)
                {
                    foreach (TransactionType transactionType in deviceTransactionTypes)
                    {
                        TransactionLog transaction = new TransactionLog()
                        {
                            TransactionType = transactionType,
                            Device = device,
                            DeviceBatch = deviceBatch
                        };
                        int deviceTransactionId = transactionManager.NewTransaction(transaction);

                        try
                        {
                            transactionManager.UpdateTransactionState(deviceTransactionId, TransactionStateLookup.ServiceStart);

                            processMeter(device, meter, deviceTransactionId, true);

                            transactionManager.UpdateTransactionState(deviceTransactionId, TransactionStateLookup.ServiceEnd);

                            FinishTransaction(deviceTransactionId, transactionType);
                        }
                        catch (Exception ex)
                        {
                            string message = string.Format(CultureInfo.InvariantCulture, Init.StringManager.GetString("MeterProcessingFailed", CultureInfo.CurrentCulture), device.EquipmentNumber);
                            Log.Error(message, ex);
                            errorMessage += message;
                            transactionManager.UpdateTransactionStatus(deviceTransactionId, (int)TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                        }
                    }
                }

                if (deviceTestTransactionTypes.Count > 0)
                {
                    IList<MeterTestResult> meterTests = StaticPersistence.WnpSystem.GetEquipmentAllTestResult<MeterTestResult>(meter.EquipmentNumber, meter.Owner.Id);
                    IList<DateTime> uniqueTestStarts = meterTests.Select(x => x.TestDate).Distinct().ToList();

                    foreach (DateTime testDate in uniqueTestStarts)
                    {
                        DeviceTest deviceTest = CreateDeviceTest(device, testDate);

                        foreach (TransactionType transactionType in deviceTestTransactionTypes)
                        {
                            TransactionLog transaction = new TransactionLog()
                            {
                                TransactionType = transactionType,
                                Device = device,
                                DeviceTest = deviceTest,
                                DeviceBatch = deviceBatch
                            };
                            int testTransactionId = transactionManager.NewTransaction(transaction);

                            try
                            {
                                transactionManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceStart);

                                processMeterTestResults(device, deviceTest, meter, testTransactionId);

                                transactionManager.UpdateTransactionState(testTransactionId, TransactionStateLookup.ServiceEnd);

                                FinishTransaction(testTransactionId, transactionType);
                            }
                            catch (Exception ex)
                            {
                                string message = string.Format(CultureInfo.InvariantCulture, Init.StringManager.GetString("MeterTestProcessingFailed", CultureInfo.CurrentCulture), device.EquipmentNumber, deviceTest.TestDate);
                                Log.Error(message, ex);
                                errorMessage += message;
                                transactionManager.UpdateTransactionStatus(testTransactionId, (int)TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new AggregateException(errorMessage);
            }
        }

        /// <summary>
        /// Marks transaction as skipped if meter belongs to a new batch.
        /// </summary>
        /// <param name="equipment">The equipment.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <returns>
        /// True if transaction was marked as skipped, false otherwise.
        /// </returns>
        public static bool SkipNewBatchTransaction(Equipment equipment, int transactionId)
        {
            if (equipment == null)
            {
                throw new ArgumentNullException("equipment", "Can not check if equipment belongs to new batch if it is not specified.");
            }

            if (equipment.NewBatch != null &&
                (equipment.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.New)) ||
                equipment.NewBatch.Status == char.Parse(Utilities.GetEnumDescription(NewBatchLookup.Pending))))
            {
                string message = string.Format(CultureInfo.InvariantCulture, Init.StringManager.GetString("SkipDeviceBelongsToNewBatch", CultureInfo.CurrentCulture), equipment.NewBatch.Description);
                Log.Info(message);
                transactionManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Marks transaction as skipped if there is a previous successful transaction with same hash.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// True if transaction was marked as skipped. False otherwise.
        /// </returns>
        public static bool SkipDuplicateTransaction(Device device, int transactionId, string hash)
        {
            TransactionLog currentTransaction = transactionManager.GetTransaction(transactionId);
            string previousHash = transactionManager.GetLastSuccessfulDeviceTransactionDataHash(device, currentTransaction.TransactionType);

            if (previousHash == hash)
            {
                string message = Init.StringManager.GetString("SkipDuplicate", CultureInfo.CurrentCulture);
                Log.Info(message);
                transactionManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Marks transaction as skipped if there is a previous successful transaction with same hash.
        /// </summary>
        /// <param name="deviceTest">The device test.</param>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="hash">The hash.</param>
        /// <returns>
        /// True if transaction was marked as skipped. False otherwise.
        /// </returns>
        public static bool SkipDuplicateTransaction(DeviceTest deviceTest, int transactionId, string hash)
        {
            TransactionLog currentTransaction = transactionManager.GetTransaction(transactionId);
            string previousHash = transactionManager.GetLastSuccessfulDeviceTestTransactionDataHash(deviceTest, currentTransaction.TransactionType);

            if (previousHash == hash)
            {
                string message = Init.StringManager.GetString("SkipDuplicate", CultureInfo.CurrentCulture);
                Log.Info(message);
                transactionManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Skipped, message, null);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Finishes the transaction based on it's completion setting and current state.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        /// <param name="transactionType">Type of the transaction.</param>
        private static void FinishTransaction(int transactionId, TransactionType transactionType)
        {
            TransactionLog transaction = transactionManager.GetTransaction(transactionId);
            if (transactionType.TransactionCompletion.Id == (int)TransactionCompletionLookup.Default && transaction.TransactionStatus.Id == (int)TransactionStatusLookup.InProgress)
            {
                transactionManager.UpdateTransactionStatus(transactionId, (int)TransactionStatusLookup.Succeeded);
            }
        }

        /// <summary>
        /// Creates the device object.
        /// </summary>
        /// <param name="meter">The meter.</param>
        /// <param name="company">The company.</param>
        /// <returns>
        /// The device
        /// </returns>
        private static Device CreateDevice(Meter meter, Company company)
        {
            Device device = new Device
            {
                Company = company,
                EquipmentNumber = meter.EquipmentNumber,
                EquipmentType = StaticPersistence.DeviceManager.GetEquipmentTypeByInternalCode("E", "EM")
            };

            return StaticPersistence.DeviceManager.GetOrCreateDevice(device);
        }

        /// <summary>
        /// Creates the device test.
        /// </summary>
        /// <param name="device">The device.</param>
        /// <param name="testDate">The test date.</param>
        /// <returns>The device test.</returns>
        private static DeviceTest CreateDeviceTest(Device device, DateTime testDate)
        {
            DeviceTest test = new DeviceTest()
            {
                Device = device,
                TestDate = testDate
            };

            return StaticPersistence.DeviceManager.GetOrCreateDeviceTest(test);
        }    
    }
}
