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
    using System.Resources;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.ServiceModel;
    using System.Text;
    using AMSLLC.Listener.Common;
    using AMSLLC.Listener.Common.Lookup;
    using AMSLLC.Listener.Common.Model;
    using AMSLLC.Listener.Common.WNP;
    using AMSLLC.Listener.Common.WNP.Model;
    using AMSLLC.Listener.Globalization;
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
        /// The string manager
        /// </summary>
        private ResourceManager stringManager = Init.StringManager;

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
        /// The transaction log debug message
        /// </summary>
        private string transactionLogDebugMessage;

        /// <summary>
        /// The meter barcodes
        /// </summary>
        private IList<MeterBarcode> meterBarcodes;

        /// <summary>
        /// The current transformer barcodes
        /// </summary>
        private IList<CurrentTransformerBarcode> currentTransformerBarcodes;

        /// <summary>
        /// The potential transformer barcodes
        /// </summary>
        private IList<PotentialTransformerBarcode> potentialTransformerBarcodes;

        /// <summary>
        /// The companies
        /// </summary>
        private IList<Company> companies;

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
            string userName = null;
            string group = null;

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["WebService.Service.UserName"]))
            {
                userName = ConfigurationManager.AppSettings["WebService.Service.UserName"];
            }

            if (!string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["WebService.Service.Group"]))
            {
                group = ConfigurationManager.AppSettings["WebService.Service.Group"];
            }

            PrincipalPermission principalPerm = new PrincipalPermission(userName, group);
            principalPerm.Demand();

            int transactionId = this.transactionLogManager.NewTransaction(TransactionTypeLookup.GetClassificationCodes, null, null, null, TransactionSourceLookup.WebServiceCall);
            this.transactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceStart);

            this.transactionLogDebugMessage = string.Empty;
            this.meterBarcodes = new List<MeterBarcode>();
            this.currentTransformerBarcodes = new List<CurrentTransformerBarcode>();
            this.potentialTransformerBarcodes = new List<PotentialTransformerBarcode>();
            this.companies = this.deviceManager.GetCompanies();

            try
            {
                ValidateRequest(request);

                foreach (DeviceClassificationCodeType classificationCode in request.UpdateDeviceClassificationCodeABM.UpdateDeviceClassificationCode)
                {
                    DeviceClassificationCodeType classificationCodeTransformed = TransformClassificationCode(classificationCode);
                    switch (classificationCodeTransformed.DeviceType)
                    {
                        case "MR":
                            this.ProcessMeterBarcodeClassificationCode(classificationCodeTransformed);
                            break;
                        case "CT":
                            this.ProcessCurrentTransformerBarcodeClassificationCode(classificationCodeTransformed);
                            break;
                        case "PT":
                            this.ProcessPotentialTransformerBarcodeClassificationCode(classificationCodeTransformed);
                            break;
                        default:
                            Log.Error(string.Format(CultureInfo.InvariantCulture, "Unsupported device type {0} in classification code {1}.", classificationCodeTransformed.DeviceType, classificationCodeTransformed.ClassificationCode));
                            break;
                    }
                }

                List<MeterBarcode> currentMeterBarcodes = this.wnpSystem.GetBarcodes<MeterBarcode>() as List<MeterBarcode>;
                List<MeterBarcode> updatableMeterBarcodes = currentMeterBarcodes.Intersect<MeterBarcode>(this.meterBarcodes, new BarcodeMeterIdComparer()).ToList();

                foreach (MeterBarcode meterBarcode in updatableMeterBarcodes)
                {
                    MeterBarcode sourceBarcode = currentMeterBarcodes.Single<MeterBarcode>(item => ((item.Owner.Id == meterBarcode.Owner.Id) && (item.LookupCode == meterBarcode.LookupCode)));
                    MeterBarcode targetBarcode = this.meterBarcodes.Single<MeterBarcode>(item => ((item.Owner.Id == meterBarcode.Owner.Id) && (item.LookupCode == meterBarcode.LookupCode)));
                    
                    targetBarcode.TestRevision = sourceBarcode.TestRevision;
                    targetBarcode.StandardMode = sourceBarcode.StandardMode;
                    targetBarcode.DwellTime = sourceBarcode.DwellTime;
                    targetBarcode.Optics = sourceBarcode.Optics;
                    targetBarcode.TestTime = sourceBarcode.TestTime;
                    targetBarcode.TestProgressMeasure = sourceBarcode.TestProgressMeasure;
                    targetBarcode.TestService = sourceBarcode.TestService;
                    targetBarcode.TestLimitAsFound = sourceBarcode.TestLimitAsFound;
                    targetBarcode.TestLimitAsLeft = sourceBarcode.TestLimitAsLeft;
                }

                this.wnpSystem.CleanBarcodes<MeterBarcode>();
                this.wnpSystem.UpdateBarcodes<MeterBarcode>(this.meterBarcodes);

                this.wnpSystem.CleanBarcodes<CurrentTransformerBarcode>();
                this.wnpSystem.UpdateBarcodes<CurrentTransformerBarcode>(this.currentTransformerBarcodes);

                this.wnpSystem.CleanBarcodes<PotentialTransformerBarcode>();
                this.wnpSystem.UpdateBarcodes<PotentialTransformerBarcode>(this.potentialTransformerBarcodes);

                this.transactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Succeeded);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                this.transactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, ex.Message, ex.ToString());
                throw;
            }

            if (!string.IsNullOrEmpty(this.transactionLogDebugMessage))
            {
                this.transactionLogManager.UpdateTransactionStatus(transactionId, TransactionStatusLookup.Failed, this.stringManager.GetString("ImportFailedForSomeRecords", CultureInfo.CurrentCulture), this.transactionLogDebugMessage);
            }
            else 
            {
                this.transactionLogManager.UpdateTransactionState(transactionId, TransactionStateLookup.ServiceEnd);
            }
        }

        /// <summary>
        /// Validates the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="ArgumentException">Throws exception if request is empty or any of the required fields are not present.
        /// </exception>
        private static void ValidateRequest(Update request)
        {
            string message;

            if (request == null)
            {
                message = "Can not process request because it is null.";
                Log.Error(message);
                throw new ArgumentException(message);
            }

            UpdateDeviceClassificationCodeABMType alliantRequest = request.UpdateDeviceClassificationCodeABM;
            if (alliantRequest == null)
            {
                message = "Can not process request because UpdateDeviceClassificationCodeABM is null.";
                Log.Error(message);
                throw new ArgumentException(message);
            }

            if (alliantRequest.BatchesTotalSpecified == false || alliantRequest.BatchNumberSpecified == false)
            {
                message = "Can not process request because BatchesTotal or BatchNumber is not specified.";
                Log.Error(message);
                throw new ArgumentException(message);
            }

            if (alliantRequest.BatchesTotal != 1 || alliantRequest.BatchNumber != 1)
            {
                message = "Batching is currently not supported. Both BatchesTotal and BatchNumber must be set to 1 and all data must be send in one request.";
                Log.Error(message);
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Performs any known valid classification code transformations so that it would pass validation and could be successfully inserted to database.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>The transformed classification code.</returns>
        private static DeviceClassificationCodeType TransformClassificationCode(DeviceClassificationCodeType classificationCode)
        {
            // Trim whitespaces on both ends and truncate string to 50 chars.
            classificationCode.DeviceDescription = classificationCode.DeviceDescription.Trim();
            if (classificationCode.DeviceDescription.Length > 50)
            {
                classificationCode.DeviceDescription = classificationCode.DeviceDescription.Substring(0, 50);
            }

            // Translate "SW" base to 'Z'
            if (classificationCode.ElectricDevice != null && classificationCode.ElectricDevice.Base == "SW")
            {
                classificationCode.ElectricDevice.Base = "Z";
            }

            return classificationCode;
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
                CustomField1 = classificationCode.Status,
                CustomField2 = classificationCode.Manufacturer,
                CustomField3 = classificationCode.Model,
                CustomField4 = classificationCode.ForceRetirementSwitch,
                CustomField5 = classificationCode.MaterialID,
                CustomField6 = classificationCode.AssetProfileID,
                CustomField7 = classificationCode.DeviceTestType,
                CustomField8 = classificationCode.TemplateDevice,
                CustomField12 = classificationCode.ElectricDevice.VoltageClass,
                CustomField13 = classificationCode.ElectricDevice.AMIIndicator,
                CustomField14 = classificationCode.ElectricDevice.ERTIndicator,
                CustomField16 = classificationCode.ElectricDevice.TransformerRatedIndicator,
                CustomField17 = classificationCode.ElectricDevice.NetworkIndicator,
                CustomField20 = classificationCode.ElectricDevice.RemoteConnectDisconnectIndicator,
                CustomField21 = classificationCode.ElectricDevice.RecorderExists,
                CustomField22 = classificationCode.ElectricDevice.LossCompensationCapableIndicator,
                Description = classificationCode.DeviceDescription,
                Form = classificationCode.ElectricDevice.Form,
                LookupCode = classificationCode.ClassificationCode,
                Owner = owner,
                RegisterRatio = classificationCode.ElectricDevice.RegisterRatio,
                TestSequence = classificationCode.ElectricDevice.TestSequence,
                
                // Default values
                TestRevision = 1,
                StandardMode = "WattHrs",
                DwellTime = 4,
                Optics = "OpticCoupler",
                TestTime = 20,
                TestProgressMeasure = 'R',
                TestService = "1 Phase",
                TestLimitAsFound = 1,
                TestLimitAsLeft = 1
            };

            if (classificationCode.ElectricDevice.TestAmpsSpecified)
            {
                result.Amp = classificationCode.ElectricDevice.TestAmps;
            }

            char tmpChar;
            if (char.TryParse(classificationCode.ElectricDevice.Base, out tmpChar))
            {
                result.Base = tmpChar;
            }

            if (classificationCode.ElectricDevice.BatteryLifeSpecified)
            {
                result.CustomField9 = classificationCode.ElectricDevice.BatteryLife.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.ElectricDevice.StatorSpecified)
            {
                result.CustomField10 = classificationCode.ElectricDevice.Stator.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.ElectricDevice.AmpacitySpecified)
            {
                result.CustomField11 = classificationCode.ElectricDevice.Ampacity.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.ElectricDevice.ConstantSpecified)
            {
                result.KH = classificationCode.ElectricDevice.Constant.ToString(CultureInfo.InvariantCulture);
            }

            int tempInt;
            if (int.TryParse(classificationCode.ElectricDevice.Phase, NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt))
            {
                result.Phase = tempInt;
            }

            if (classificationCode.ElectricDevice.TestVoltageSpecified)
            {
                result.Volt = classificationCode.ElectricDevice.TestVoltage;
            }

            if (classificationCode.ElectricDevice.WireSpecified)
            {
                result.Wire = classificationCode.ElectricDevice.Wire;
            }

            if (externalCode == "I")
            {
                result.CustomField18 = classificationCode.ElectricDevice.IPLSelectionType;
                if (classificationCode.ElectricDevice.IPLTestIntervalSpecified)
                {
                    result.CustomField19 = classificationCode.ElectricDevice.IPLTestInterval.ToString(CultureInfo.InvariantCulture);
                }
            }

            if (externalCode == "W")
            {
                result.CustomField18 = classificationCode.ElectricDevice.WPLSelectionType;
                if (classificationCode.ElectricDevice.WPLTestIntervalSpecified)
                {
                    result.CustomField19 = classificationCode.ElectricDevice.WPLTestInterval.ToString(CultureInfo.InvariantCulture);
                }
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
            };

            decimal tempFloat;
            int tempInt;
            if (decimal.TryParse(classificationCode.TransformerAttribute.CurrentTransformer.OhmsBurden1, NumberStyles.Float, CultureInfo.InvariantCulture, out tempFloat))
            {
                result.Burden1 = tempFloat;
            }

            if (decimal.TryParse(classificationCode.TransformerAttribute.CurrentTransformer.OhmsBurden2, NumberStyles.Float, CultureInfo.InvariantCulture, out tempFloat))
            {
                result.Burden2 = tempFloat;
            }

            if (classificationCode.TransformerAttribute.AccuracyClassSpecified)
            {
                result.AccuracyClass1 = classificationCode.TransformerAttribute.AccuracyClass;
            }

            if (classificationCode.TransformerAttribute.BasicLightingImpulseLevelSpecified)
            {
                result.CustomField10 = classificationCode.TransformerAttribute.BasicLightingImpulseLevel.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.InsulationVoltageClassSpecified)
            {
                decimal tmp = classificationCode.TransformerAttribute.InsulationVoltageClass / 10;
                result.CustomField11 = tmp.ToString("0.0 KV", CultureInfo.InvariantCulture);
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
            
            if (int.TryParse(classificationCode.TransformerAttribute.NumberOfRatios, NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt))
            {
                result.Taps = tempInt;

                switch (result.Taps)
                {
                    case 1:
                        result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}:5", classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio1);
                        break;
                    case 2:
                        result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}/{1}:5", classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio1, classificationCode.TransformerAttribute.CurrentTransformer.PrimaryCurrentRatio2);
                        break;
                }
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
                CustomField14 = classificationCode.TransformerAttribute.PotentialTransformer.Fused,
                CustomField15 = classificationCode.TransformerAttribute.PotentialTransformer.DualSecondary,
                Description = classificationCode.DeviceDescription,
                LookupCode = classificationCode.ClassificationCode,
                Owner = owner,
            };

            if (classificationCode.TransformerAttribute.AccuracyClassSpecified)
            {
                result.AccuracyClass1 = classificationCode.TransformerAttribute.AccuracyClass;
            }

            if (classificationCode.TransformerAttribute.BasicLightingImpulseLevelSpecified)
            {
                result.CustomField10 = classificationCode.TransformerAttribute.BasicLightingImpulseLevel.ToString(CultureInfo.InvariantCulture);
            }

            if (classificationCode.TransformerAttribute.InsulationVoltageClassSpecified)
            {
                decimal tmp = classificationCode.TransformerAttribute.InsulationVoltageClass / 10;
                result.CustomField11 = tmp.ToString("0.0 KV", CultureInfo.InvariantCulture);
            }

            int tempInt;
            if (int.TryParse(classificationCode.TransformerAttribute.NumberOfRatios, NumberStyles.Integer, CultureInfo.InvariantCulture, out tempInt))
            {
                result.Taps = tempInt;

                switch (result.Taps)
                {
                    case 1:
                        result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}:1", classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio1);
                        break;
                    case 2:
                        result.Ratio = string.Format(CultureInfo.InvariantCulture, "{0}/{1}:1", classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio1, classificationCode.TransformerAttribute.PotentialTransformer.PrimaryVoltageRatio2);
                        break;
                }
            }

            return result;
        }

        /// <summary>
        /// Checks if classification code is valid.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>True if classification code is valid and should be included in WNP database. False otherwise.</returns>
        private bool ClassificationCodeValid(DeviceClassificationCodeType classificationCode)
        {
            bool valid = true;
            if (classificationCode.DeviceDescription.Length > 50)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a description longer than 50 symbols: {1}", classificationCode.ClassificationCode, classificationCode.DeviceDescription);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (classificationCode.ElectricDevice != null && !this.ElectricMeterClassificationCodeValid(classificationCode))
            {
                valid = false;
            }

            if (classificationCode.TransformerAttribute != null && !this.TransformerClassificationCodeValid(classificationCode))
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks if electric meter classification code is valid.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>True if classification code is valid and should be included in WNP database. False otherwise.</returns>
        private bool ElectricMeterClassificationCodeValid(DeviceClassificationCodeType classificationCode)
        {
            bool valid = true;

            DeviceClassificationCodeTypeElectricDevice meter = classificationCode.ElectricDevice;
            if (meter.Base.Length > 1)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a Base value longer than 1 symbol: {1}", classificationCode.ClassificationCode, meter.Base);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (meter.RegisterRatio.Length > 11)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a RegisterRatio value longer than 11 symbol: {1}", classificationCode.ClassificationCode, meter.RegisterRatio);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (meter.TestSequence.Length > 50)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a TestSequence value longer than 50 symbol: {1}", classificationCode.ClassificationCode, meter.TestSequence);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks if transformer classification code is valid.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>True if classification code is valid and should be included in WNP database. False otherwise.</returns>
        private bool TransformerClassificationCodeValid(DeviceClassificationCodeType classificationCode)
        {
            bool valid = true;
            DeviceClassificationCodeTypeTransformerAttribute transformer = classificationCode.TransformerAttribute;

            if (transformer.NumberOfRatios != "1" && transformer.NumberOfRatios != "2")
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has incorrect NumberOfRatios value set to {1}, allowed values are 1 or 2.", classificationCode.ClassificationCode, transformer.NumberOfRatios);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.PotentialTransformer != null && !this.PotentialTransformerClassificationCodeValid(classificationCode))
            {
                valid = false;
            }                

            if (transformer.CurrentTransformer != null && !this.CurrentTransformerClassificationCodeValid(classificationCode))
            {
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks if potential transformer classification code is valid.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>True if classification code is valid and should be included in WNP database. False otherwise.</returns>
        private bool PotentialTransformerClassificationCodeValid(DeviceClassificationCodeType classificationCode)
        {
            bool valid = true;

            DeviceClassificationCodeTypeTransformerAttribute transformer = classificationCode.TransformerAttribute;
            DeviceClassificationCodeTypeTransformerAttributePotentialTransformer potentialTransformer = transformer.PotentialTransformer;

            if (potentialTransformer.VoltAmpsBurden1.Length > 2)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a VoltAmpsBurden1 value longer than 2 symbols: {1}", classificationCode.ClassificationCode, potentialTransformer.VoltAmpsBurden1);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (potentialTransformer.VoltAmpsBurden2.Length > 2)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a VoltAmpsBurden2 value longer than 2 symbols: {1}", classificationCode.ClassificationCode, potentialTransformer.VoltAmpsBurden2);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "1" && (string.IsNullOrWhiteSpace(potentialTransformer.PrimaryVoltageRatio1) || potentialTransformer.PrimaryVoltageRatio1 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 1, but PrimaryVoltageRatio1 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "1" && !(string.IsNullOrWhiteSpace(potentialTransformer.PrimaryVoltageRatio2) || potentialTransformer.PrimaryVoltageRatio2 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 1, but PrimaryVoltageRatio2 has value {1}.", classificationCode.ClassificationCode, potentialTransformer.PrimaryVoltageRatio2);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "2" && (string.IsNullOrWhiteSpace(potentialTransformer.PrimaryVoltageRatio1) || potentialTransformer.PrimaryVoltageRatio1 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 2, but PrimaryVoltageRatio1 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "2" && (string.IsNullOrWhiteSpace(potentialTransformer.PrimaryVoltageRatio2) || potentialTransformer.PrimaryVoltageRatio2 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 2, but PrimaryVoltageRatio2 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Checks if current transformer classification code is valid.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <returns>True if classification code is valid and should be included in WNP database. False otherwise.</returns>
        private bool CurrentTransformerClassificationCodeValid(DeviceClassificationCodeType classificationCode)
        {
            bool valid = true;

            DeviceClassificationCodeTypeTransformerAttribute transformer = classificationCode.TransformerAttribute;
            DeviceClassificationCodeTypeTransformerAttributeCurrentTransformer currentTransformer = transformer.CurrentTransformer;

            if (transformer.NumberOfRatios == "1" && (string.IsNullOrWhiteSpace(currentTransformer.PrimaryCurrentRatio1) || currentTransformer.PrimaryCurrentRatio1 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 1, but PrimaryCurrentRatio1 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "1" && !(string.IsNullOrWhiteSpace(currentTransformer.PrimaryCurrentRatio2) || currentTransformer.PrimaryCurrentRatio2 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 1, but PrimaryCurrentRatio2 has value {1}.", classificationCode.ClassificationCode, currentTransformer.PrimaryCurrentRatio2);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "2" && (string.IsNullOrWhiteSpace(currentTransformer.PrimaryCurrentRatio1) || currentTransformer.PrimaryCurrentRatio1 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 2, but PrimaryCurrentRatio1 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            if (transformer.NumberOfRatios == "2" && (string.IsNullOrWhiteSpace(currentTransformer.PrimaryCurrentRatio2) || currentTransformer.PrimaryCurrentRatio2 == "0"))
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, has a NumberOfRatios value set to 2, but PrimaryCurrentRatio2 is not specified.", classificationCode.ClassificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
                valid = false;
            }

            return valid;
        }

        /// <summary>
        /// Processes the meter classification code.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        private void ProcessMeterBarcodeClassificationCode(DeviceClassificationCodeType classificationCode)
        {
            if (this.ClassificationCodeValid(classificationCode))
            {
                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    MeterBarcode meterBarcode = CreateMeterBarcodeRecord(classificationCode, owner, company.ExternalCode);
                    if (meterBarcode != null)
                    {
                        this.meterBarcodes.Add(meterBarcode);
                    }
                }
            }
            else
            {
                bool found = false;

                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    MeterBarcode barcode = new MeterBarcode()
                    {
                        LookupCode = classificationCode.ClassificationCode,
                        Owner = owner
                    };
                    MeterBarcode existingBarcode = this.wnpSystem.GetBarcode<MeterBarcode>(barcode);
                    if (existingBarcode != null)
                    {
                        this.meterBarcodes.Add(existingBarcode);
                        found = true;
                    }
                }

                this.LogBarcodeImportFailure(classificationCode.ClassificationCode, found);
            }
        }

        /// <summary>
        /// Processes the current transformer classification code.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        private void ProcessCurrentTransformerBarcodeClassificationCode(DeviceClassificationCodeType classificationCode)
        {
            if (this.ClassificationCodeValid(classificationCode))
            {
                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    CurrentTransformerBarcode currentTransformerBarcode = CreateCurrentTransformerBarcodeRecord(classificationCode, owner);
                    if (currentTransformerBarcode != null)
                    {
                        this.currentTransformerBarcodes.Add(currentTransformerBarcode);
                    }
                }
            }
            else
            {
                bool found = false;

                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    CurrentTransformerBarcode barcode = new CurrentTransformerBarcode()
                    {
                        LookupCode = classificationCode.ClassificationCode,
                        Owner = owner
                    };
                    CurrentTransformerBarcode existingBarcode = this.wnpSystem.GetBarcode<CurrentTransformerBarcode>(barcode);
                    if (existingBarcode != null)
                    {
                        this.currentTransformerBarcodes.Add(existingBarcode);
                        found = true;
                    }
                }

                this.LogBarcodeImportFailure(classificationCode.ClassificationCode, found);
            }
        }

        /// <summary>
        /// Processes the potential transformer classification code.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        private void ProcessPotentialTransformerBarcodeClassificationCode(DeviceClassificationCodeType classificationCode)
        {
            if (this.ClassificationCodeValid(classificationCode))
            {
                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    PotentialTransformerBarcode potentialTransformerBarcode = CreatePotentialTransformerBarcodeRecord(classificationCode, owner);
                    if (potentialTransformerBarcode != null)
                    {
                        this.potentialTransformerBarcodes.Add(potentialTransformerBarcode);
                    }
                }
            }
            else
            {
                bool found = false;

                foreach (Company company in this.companies)
                {
                    Owner owner = new Owner(int.Parse(company.InternalCode, CultureInfo.InvariantCulture));
                    PotentialTransformerBarcode barcode = new PotentialTransformerBarcode()
                    {
                        LookupCode = classificationCode.ClassificationCode,
                        Owner = owner
                    };
                    PotentialTransformerBarcode existingBarcode = this.wnpSystem.GetBarcode<PotentialTransformerBarcode>(barcode);
                    if (existingBarcode != null)
                    {
                        this.potentialTransformerBarcodes.Add(existingBarcode);
                        found = true;
                    }
                }

                this.LogBarcodeImportFailure(classificationCode.ClassificationCode, found);
            }
        }

        /// <summary>
        /// Logs error about failed classification code update or import.
        /// </summary>
        /// <param name="classificationCode">The classification code.</param>
        /// <param name="notUpdated">If set to <c>true</c> classification code not updated message is logged. Else classification code not imported message is logged.</param>
        private void LogBarcodeImportFailure(string classificationCode, bool notUpdated)
        {
            if (notUpdated)
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, will not be updated.", classificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
            }
            else 
            {
                string message = string.Format(CultureInfo.InvariantCulture, "Classification Code {0}, will not be imported.", classificationCode);
                Log.Error(message);
                this.transactionLogDebugMessage += string.Format(CultureInfo.InvariantCulture, "{0}{1}", message, Environment.NewLine);
            }
        }
    }
}
