//-----------------------------------------------------------------------
// <copyright file="UpdateClassificationCode.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.Alliant
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using log4net;

    /// <summary>
    /// Implementation of classification code update web service.
    /// </summary>
    public class UpdateClassificationCode : DeviceClassificationCode
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The transaction log manager
        /// </summary>
        private ITransactionManager transactionLogManager;

        /// <summary>
        /// The device manager
        /// </summary>
        private IDeviceManager deviceManager;

        /// <summary>
        /// The WNP system
        /// </summary>
        private WNPSystem wnpSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateClassificationCode"/> class.
        /// </summary>
        public UpdateClassificationCode()
        {
            using (IPersistenceManager persistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["ListenerDb"].ConnectionString))
            {
                using (IPersistenceManager clientPersistenceManager = new PersistenceManager(ConfigurationManager.ConnectionStrings["WnpDb"].ConnectionString))
                {
                    IWNPPersistenceController persistenceController = new WNPPersistenceController();
                    persistenceController.InitializeListenerSystems(persistenceManager);
                    persistenceController.InitializeListenerClientSystems(clientPersistenceManager);
                    this.transactionLogManager = new TransactionManager(persistenceController);
                    this.deviceManager = new DeviceManager(persistenceController);
                    this.wnpSystem = persistenceController.WNPSystem;
                }
            }
        }
        
        /// <summary>
        /// Updates the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ServiceModel.FaultException">
        /// Can not process request because it is null.
        /// or
        /// Can not process request because UpdateDeviceClassificationCodeABM is null.
        /// or
        /// Can not process request because BatchesTotal or BatchNumber is not specified.
        /// or
        /// </exception>
        public void Update(Update request)
        {
            if (request == null)
            {
                throw new FaultException("Can not process request because it is null.");
            }

            UpdateDeviceClassificationCodeABMType alliantRequest = request.UpdateDeviceClassificationCodeABM;
            if (alliantRequest == null)
            {
                throw new FaultException("Can not process request because UpdateDeviceClassificationCodeABM is null.");
            }

            if (alliantRequest.BatchesTotalSpecified == false || alliantRequest.BatchNumberSpecified == false)
            {
                throw new FaultException("Can not process request because BatchesTotal or BatchNumber is not specified.");
            }

            if (alliantRequest.BatchesTotal != 1 || alliantRequest.BatchNumber != 1)
            {
                throw new FaultException("Batching is currently not supported. Both BatchesTotal and BatchNumber must be set to 1 and all data must be send in one request.");
            }

            int transactionId = this.transactionLogManager.NewTransaction(TransactionTypeLookup.GetClassificationCodes, null, null, null, TransactionSourceLookup.WebServiceCall);
            this.transactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceStart);

            IList<MeterBarcode> meterBarcodes = new List<MeterBarcode>();
            IList<CurrentTransformerBarcode> currentTransformerBarcodes = new List<CurrentTransformerBarcode>();
            IList<PotentialTransformerBarcode> potentialTransformerBarcodes = new List<PotentialTransformerBarcode>();
            IList<Company> companies = this.deviceManager.GetCompanies();

            try
            {
                foreach (Company company in companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));

                    foreach (DeviceClassificationCodeType classificationCode in alliantRequest.UpdateDeviceClassificationCode)
                    {
                        switch (classificationCode.DeviceType)
                        {
                            case "MR":
                                MeterBarcode meterBarcode = CreateMeterBarcodeRecord(classificationCode, owner, company.ExternalCode);
                                if (meterBarcode != null)
                                {
                                    meterBarcodes.Add(meterBarcode);
                                }

                                break;
                            case "CT":
                                CurrentTransformerBarcode currentTransformerBarcode = CreateCurrentTransformerBarcodeRecord(classificationCode, owner);
                                if (currentTransformerBarcode != null)
                                {
                                    currentTransformerBarcodes.Add(currentTransformerBarcode);
                                }

                                break;
                            case "PT":
                                PotentialTransformerBarcode potentialTransformerBarcode = CreatePotentialTransformerBarcodeRecord(classificationCode, owner);
                                if (potentialTransformerBarcode != null)
                                {
                                    potentialTransformerBarcodes.Add(potentialTransformerBarcode);
                                }

                                break;
                            default:
                                Log.Error(string.Format(CultureInfo.InvariantCulture, "Unsupported device type {0} in classification code {1}.", classificationCode.DeviceType, classificationCode.ClassificationCode));
                                break;
                        }
                    }
                }

                List<MeterBarcode> currentMeterBarcodes = this.wnpSystem.GetBarcodes<MeterBarcode>() as List<MeterBarcode>;
                List<MeterBarcode> diff = currentMeterBarcodes.Intersect<MeterBarcode>(meterBarcodes, new BarcodeMeterIdComparer()).ToList();

                foreach (MeterBarcode meterBarcode in diff)
                {
                    MeterBarcode sourceBarcode = currentMeterBarcodes.Single<MeterBarcode>(item => ((item.Owner.Id == meterBarcode.Owner.Id) && (item.LookupCode == meterBarcode.LookupCode)));
                    MeterBarcode targetBarcode = meterBarcodes.Single<MeterBarcode>(item => ((item.Owner.Id == meterBarcode.Owner.Id) && (item.LookupCode == meterBarcode.LookupCode)));
                    
                    targetBarcode.TestRevision = sourceBarcode.TestRevision;
                    targetBarcode.StandardMode = sourceBarcode.StandardMode;
                    targetBarcode.DwellTime = sourceBarcode.DwellTime;
                    targetBarcode.Optics = sourceBarcode.Optics;
                    targetBarcode.TestTime = sourceBarcode.TestTime;
                    targetBarcode.TestProgressMeasure = sourceBarcode.TestProgressMeasure;
                    targetBarcode.TestService = sourceBarcode.TestService;
                }

                this.wnpSystem.CleanBarcodes<MeterBarcode>();
                this.wnpSystem.UpdateBarcodes<MeterBarcode>(meterBarcodes);

                this.wnpSystem.CleanBarcodes<CurrentTransformerBarcode>();
                this.wnpSystem.UpdateBarcodes<CurrentTransformerBarcode>(currentTransformerBarcodes);

                this.wnpSystem.CleanBarcodes<PotentialTransformerBarcode>();
                this.wnpSystem.UpdateBarcodes<PotentialTransformerBarcode>(potentialTransformerBarcodes);

                this.transactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Succeeded);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                this.transactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                throw;
            }

            this.transactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceEnd);
        }

        /// <summary>
        /// Creates the meter barcode record from classificationCode entry.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <param name="owner">The WNP owner.</param>
        /// <param name="externalCode">The external code.</param>
        /// <returns>
        /// The meter barcode
        /// </returns>
        private static MeterBarcode CreateMeterBarcodeRecord(DeviceClassificationCodeType classificationCode, Owner owner, string externalCode)
        {
            if (classificationCode.ElectricDevice == null)
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "ElectricDevice information is missing in meter classification code {0}", classificationCode.ClassificationCode));
                return null;
            }

            MeterBarcode result = new MeterBarcode()
            {
                Amp = (float)classificationCode.ElectricDevice.TestAmps,
                Base = char.Parse(classificationCode.ElectricDevice.Base),
                CustomField1 = classificationCode.Status,
                CustomField2 = classificationCode.Manufacturer,
                CustomField3 = classificationCode.Model,
                CustomField4 = classificationCode.ForceRetirementSwitch,
                CustomField5 = classificationCode.MaterialID,
                CustomField6 = classificationCode.AssetProfileID,
                CustomField7 = classificationCode.DeviceTestType,
                CustomField8 = classificationCode.TemplateDevice,
                CustomField9 = classificationCode.ElectricDevice.BatteryLife.ToString(CultureInfo.InvariantCulture),
                CustomField10 = classificationCode.ElectricDevice.Stator.ToString(CultureInfo.InvariantCulture),
                CustomField11 = classificationCode.ElectricDevice.Ampacity.ToString(CultureInfo.InvariantCulture),
                CustomField12 = classificationCode.ElectricDevice.VoltageClass,
                CustomField13 = classificationCode.ElectricDevice.AMIIndicator,
                CustomField14 = classificationCode.ElectricDevice.ERTIndicator,
                CustomField15 = classificationCode.ElectricDevice.TestSequence,
                CustomField16 = classificationCode.ElectricDevice.TransformerRatedIndicator,
                CustomField17 = classificationCode.ElectricDevice.NetworkIndicator,
                CustomField20 = classificationCode.ElectricDevice.RemoteConnectDisconnectIndicator,
                CustomField21 = classificationCode.ElectricDevice.RecorderExists,
                CustomField22 = classificationCode.ElectricDevice.LossCompensationCapableIndicator,
                Description = classificationCode.DeviceDescription,
                Form = classificationCode.ElectricDevice.Form,
                KH = classificationCode.ElectricDevice.Constant.ToString(CultureInfo.InvariantCulture),
                LookupCode = classificationCode.ClassificationCode,
                Owner = owner,
                Phase = int.Parse(classificationCode.ElectricDevice.Phase, CultureInfo.InvariantCulture),
                RegisterRatio = classificationCode.ElectricDevice.RegisterRatio,
                Volt = (float)classificationCode.ElectricDevice.TestVoltage,
                Wire = classificationCode.ElectricDevice.Wire,
                
                // Default values
                TestRevision = 1,
                StandardMode = "WattHrs",
                DwellTime = 4,
                Optics = "OpticCoupler",
                TestTime = 20,
                TestProgressMeasure = 'R',
                TestService = "1 Phase",
                TestSequence = "Default",
                TestLimitAsFound = 1,
                TestLimitAsLeft = 1
            };

            if (externalCode == "I")
            {
                result.CustomField18 = classificationCode.ElectricDevice.IPLSelectionType;
                result.CustomField19 = classificationCode.ElectricDevice.IPLTestInterval.ToString(CultureInfo.InvariantCulture);
            }

            if (externalCode == "W")
            {
                result.CustomField18 = classificationCode.ElectricDevice.WPLSelectionType;
                result.CustomField19 = classificationCode.ElectricDevice.WPLTestInterval.ToString(CultureInfo.InvariantCulture);
            }

            return result;
        }

        /// <summary>
        /// Creates the current transformer barcode record.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>The current transformer barcode</returns>
        private static CurrentTransformerBarcode CreateCurrentTransformerBarcodeRecord(DeviceClassificationCodeType classificationCode, Owner owner)
        {
            if (classificationCode.TransformerAttribute == null)
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "TransformerAttribute information is missing in current transofrmer classification code {0}", classificationCode.ClassificationCode));
                return null;
            }

            if (classificationCode.TransformerAttribute.CurrentTransformer == null)
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "TransformerAttribute.CurrentTransformer information is missing in current transofrmer classification code {0}", classificationCode.ClassificationCode));
                return null;
            }

            CurrentTransformerBarcode result = new CurrentTransformerBarcode()
            {
                Burden1 = float.Parse(classificationCode.TransformerAttribute.CurrentTransformer.OhmsBurden1, CultureInfo.InvariantCulture),
                Burden2 = float.Parse(classificationCode.TransformerAttribute.CurrentTransformer.OhmsBurden2, CultureInfo.InvariantCulture),
                CustomField1 = classificationCode.Status,
                CustomField2 = classificationCode.Manufacturer,
                CustomField3 = classificationCode.Model,
                CustomField4 = classificationCode.ForceRetirementSwitch,
                CustomField5 = classificationCode.MaterialID,
                CustomField6 = classificationCode.AssetProfileID,
                CustomField7 = classificationCode.DeviceTestType,
                CustomField8 = classificationCode.TemplateDevice,
                CustomField9 = classificationCode.TransformerAttribute.ConstructionType,
                CustomField12 = classificationCode.TransformerAttribute.InsulatingMedium,
                CustomField13 = classificationCode.TransformerAttribute.DeviceApplicationEnvironment,
                Description = classificationCode.DeviceDescription,
                LookupCode = classificationCode.ClassificationCode,
                Owner = owner,
                Taps = int.Parse(classificationCode.TransformerAttribute.NumberOfRatios, CultureInfo.InvariantCulture)
            };

            if (classificationCode.TransformerAttribute.AccuracyClassSpecified)
            {
                result.AccuracyClass1 = (float)classificationCode.TransformerAttribute.AccuracyClass;
            }

            if (classificationCode.TransformerAttribute.BasicLightingImpulseLevelSpecified)
            {
                result.CustomField10 = classificationCode.TransformerAttribute.BasicLightingImpulseLevel.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.InsulationVoltageClassSpecified)
            {
                result.CustomField11 = classificationCode.TransformerAttribute.InsulationVoltageClass.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.CurrentTransformer.Ratio1RatingFactorSpecified)
            {
                result.CustomField14 = classificationCode.TransformerAttribute.CurrentTransformer.Ratio1RatingFactor.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.CurrentTransformer.Ratio2RatingFactorSpecified)
            {
                result.CustomField15 = classificationCode.TransformerAttribute.CurrentTransformer.Ratio2RatingFactor.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.CurrentTransformer.LightLoadPercentageSpecified)
            {
                result.CustomField16 = classificationCode.TransformerAttribute.CurrentTransformer.LightLoadPercentage.ToString(CultureInfo.InvariantCulture);
            }

            switch (result.Taps)
            {
                case 1:
                    result.Ratio = classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio1;
                    break;
                case 2:
                    string ratio1 = classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio1;
                    ratio1 = ratio1.Substring(0, Math.Max(ratio1.IndexOf(':'), 0));
                    result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", ratio1, classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio2);
                    break;
                default:
                    Log.Error(string.Format(CultureInfo.InvariantCulture, "Can not determine current transformer classification code {0} ratio, because only 1 or 2 is allowed for number of ratios, but actual value was {1}", classificationCode.ClassificationCode, result.Taps));
                    break;
            }           

            return result;
        }

        /// <summary>
        /// Creates the potential transformer barcode record.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <param name="owner">The owner.</param>
        /// <returns>The potential transformer barcode</returns>
        private static PotentialTransformerBarcode CreatePotentialTransformerBarcodeRecord(DeviceClassificationCodeType classificationCode, Owner owner)
        {
            if (classificationCode.TransformerAttribute == null)
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "TransformerAttribute information is missing in potential transofrmer classification code {0}", classificationCode.ClassificationCode));
                return null;
            }

            if (classificationCode.TransformerAttribute.PotentialTransformer == null)
            {
                Log.Error(string.Format(CultureInfo.InvariantCulture, "TransformerAttribute.PotentialTransformer information is missing in potential transofrmer classification code {0}", classificationCode.ClassificationCode));
                return null;
            }

            PotentialTransformerBarcode result = new PotentialTransformerBarcode()
            {
                Burden1 = classificationCode.TransformerAttribute.PotentialTransformer.VoltAmpsBurden1,
                Burden2 = classificationCode.TransformerAttribute.PotentialTransformer.VoltAmpsBurden2,
                CustomField1 = classificationCode.Status,
                CustomField2 = classificationCode.Manufacturer,
                CustomField3 = classificationCode.Model,
                CustomField4 = classificationCode.ForceRetirementSwitch,
                CustomField5 = classificationCode.MaterialID,
                CustomField6 = classificationCode.AssetProfileID,
                CustomField7 = classificationCode.DeviceTestType,
                CustomField8 = classificationCode.TemplateDevice,
                CustomField9 = classificationCode.TransformerAttribute.ConstructionType,
                CustomField12 = classificationCode.TransformerAttribute.InsulatingMedium,
                CustomField13 = classificationCode.TransformerAttribute.DeviceApplicationEnvironment,
                Description = classificationCode.DeviceDescription,
                LookupCode = classificationCode.ClassificationCode,
                Owner = owner,
                Taps = int.Parse(classificationCode.TransformerAttribute.NumberOfRatios, CultureInfo.InvariantCulture)
            };

            if (classificationCode.TransformerAttribute.AccuracyClassSpecified)
            {
                result.AccuracyClass1 = (float)classificationCode.TransformerAttribute.AccuracyClass;
            }

            if (classificationCode.TransformerAttribute.BasicLightingImpulseLevelSpecified)
            {
                result.CustomField10 = classificationCode.TransformerAttribute.BasicLightingImpulseLevel.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.InsulationVoltageClassSpecified)
            {
                result.CustomField11 = classificationCode.TransformerAttribute.InsulationVoltageClass.ToString(CultureInfo.InvariantCulture);
            }

            switch (result.Taps)
            {
                case 1:
                    result.Ratio = classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio1;
                    break;
                case 2:
                    string ratio1 = classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio1;
                    ratio1 = ratio1.Substring(0, Math.Max(ratio1.IndexOf(':'), 0));
                    result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", ratio1, classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio2);
                    break;
                default:
                    Log.Error(string.Format(CultureInfo.InvariantCulture, "Can not determine potential transformer classification code {0} ratio, because only 1 or 2 is allowed for number of ratios, but actual value was {1}", classificationCode.ClassificationCode, result.Taps));
                    break;
            }

            return result;
        }
    }
}
