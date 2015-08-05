﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#pragma warning disable CS1591

namespace AMSLLC.Listener.Service.Implementation.Alliant
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1", ConfigurationName = "DeviceClassificationCode")]
    public interface DeviceClassificationCode
    {

        // CODEGEN: Generating message contract since the operation Update is neither RPC nor document wrapped.
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1/Update" +
            "", Name="Update")]
        [System.ServiceModel.XmlSerializerFormatAttribute()]
        void Update(Update request);
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class UpdateDeviceClassificationCodeABMType
    {

        private int batchNumberField;

        private bool batchNumberFieldSpecified;

        private int batchesTotalField;

        private bool batchesTotalFieldSpecified;

        private DeviceClassificationCodeType[] updateDeviceClassificationCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public int BatchNumber
        {
            get
            {
                return this.batchNumberField;
            }
            set
            {
                this.batchNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BatchNumberSpecified
        {
            get
            {
                return this.batchNumberFieldSpecified;
            }
            set
            {
                this.batchNumberFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int BatchesTotal
        {
            get
            {
                return this.batchesTotalField;
            }
            set
            {
                this.batchesTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BatchesTotalSpecified
        {
            get
            {
                return this.batchesTotalFieldSpecified;
            }
            set
            {
                this.batchesTotalFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("UpdateDeviceClassificationCode", Order = 2)]
        public DeviceClassificationCodeType[] UpdateDeviceClassificationCode
        {
            get
            {
                return this.updateDeviceClassificationCodeField;
            }
            set
            {
                this.updateDeviceClassificationCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class DeviceClassificationCodeType
    {

        private string classificationCodeField;

        private string deviceDescriptionField;

        private string deviceTypeField;

        private string statusField;

        private string manufacturerField;

        private string modelField;

        private string forceRetirementSwitchField;

        private string materialIDField;

        private string assetProfileIDField;

        private string deviceTestTypeField;

        private string templateDeviceField;

        private DeviceClassificationCodeTypeElectricDevice electricDeviceField;

        private DeviceClassificationCodeTypeTransformerAttribute transformerAttributeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string ClassificationCode
        {
            get
            {
                return this.classificationCodeField;
            }
            set
            {
                this.classificationCodeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string DeviceDescription
        {
            get
            {
                return this.deviceDescriptionField;
            }
            set
            {
                this.deviceDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string DeviceType
        {
            get
            {
                return this.deviceTypeField;
            }
            set
            {
                this.deviceTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string Manufacturer
        {
            get
            {
                return this.manufacturerField;
            }
            set
            {
                this.manufacturerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string ForceRetirementSwitch
        {
            get
            {
                return this.forceRetirementSwitchField;
            }
            set
            {
                this.forceRetirementSwitchField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string MaterialID
        {
            get
            {
                return this.materialIDField;
            }
            set
            {
                this.materialIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public string AssetProfileID
        {
            get
            {
                return this.assetProfileIDField;
            }
            set
            {
                this.assetProfileIDField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string DeviceTestType
        {
            get
            {
                return this.deviceTestTypeField;
            }
            set
            {
                this.deviceTestTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public string TemplateDevice
        {
            get
            {
                return this.templateDeviceField;
            }
            set
            {
                this.templateDeviceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public DeviceClassificationCodeTypeElectricDevice ElectricDevice
        {
            get
            {
                return this.electricDeviceField;
            }
            set
            {
                this.electricDeviceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public DeviceClassificationCodeTypeTransformerAttribute TransformerAttribute
        {
            get
            {
                return this.transformerAttributeField;
            }
            set
            {
                this.transformerAttributeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class DeviceClassificationCodeTypeElectricDevice
    {

        private string iPLSelectionTypeField;

        private int iPLTestIntervalField;

        private bool iPLTestIntervalFieldSpecified;

        private string wPLSelectionTypeField;

        private int wPLTestIntervalField;

        private bool wPLTestIntervalFieldSpecified;

        private int batteryLifeField;

        private bool batteryLifeFieldSpecified;

        private int wireField;

        private bool wireFieldSpecified;

        private decimal statorField;

        private bool statorFieldSpecified;

        private int ampacityField;

        private bool ampacityFieldSpecified;

        private decimal testAmpsField;

        private bool testAmpsFieldSpecified;

        private string voltageClassField;

        private int testVoltageField;

        private bool testVoltageFieldSpecified;

        private decimal constantField;

        private bool constantFieldSpecified;

        private string phaseField;

        private string formField;

        private string baseField;

        private string registerRatioField;

        private string aMIIndicatorField;

        private string eRTIndicatorField;

        private string transformerRatedIndicatorField;

        private string networkIndicatorField;

        private string remoteConnectDisconnectIndicatorField;

        private string recorderExistsField;

        private string lossCompensationCapableIndicatorField;

        private string testSequenceField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string IPLSelectionType
        {
            get
            {
                return this.iPLSelectionTypeField;
            }
            set
            {
                this.iPLSelectionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public int IPLTestInterval
        {
            get
            {
                return this.iPLTestIntervalField;
            }
            set
            {
                this.iPLTestIntervalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IPLTestIntervalSpecified
        {
            get
            {
                return this.iPLTestIntervalFieldSpecified;
            }
            set
            {
                this.iPLTestIntervalFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string WPLSelectionType
        {
            get
            {
                return this.wPLSelectionTypeField;
            }
            set
            {
                this.wPLSelectionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public int WPLTestInterval
        {
            get
            {
                return this.wPLTestIntervalField;
            }
            set
            {
                this.wPLTestIntervalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool WPLTestIntervalSpecified
        {
            get
            {
                return this.wPLTestIntervalFieldSpecified;
            }
            set
            {
                this.wPLTestIntervalFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public int BatteryLife
        {
            get
            {
                return this.batteryLifeField;
            }
            set
            {
                this.batteryLifeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BatteryLifeSpecified
        {
            get
            {
                return this.batteryLifeFieldSpecified;
            }
            set
            {
                this.batteryLifeFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public int Wire
        {
            get
            {
                return this.wireField;
            }
            set
            {
                this.wireField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool WireSpecified
        {
            get
            {
                return this.wireFieldSpecified;
            }
            set
            {
                this.wireFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public decimal Stator
        {
            get
            {
                return this.statorField;
            }
            set
            {
                this.statorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool StatorSpecified
        {
            get
            {
                return this.statorFieldSpecified;
            }
            set
            {
                this.statorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public int Ampacity
        {
            get
            {
                return this.ampacityField;
            }
            set
            {
                this.ampacityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AmpacitySpecified
        {
            get
            {
                return this.ampacityFieldSpecified;
            }
            set
            {
                this.ampacityFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public decimal TestAmps
        {
            get
            {
                return this.testAmpsField;
            }
            set
            {
                this.testAmpsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TestAmpsSpecified
        {
            get
            {
                return this.testAmpsFieldSpecified;
            }
            set
            {
                this.testAmpsFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
        public string VoltageClass
        {
            get
            {
                return this.voltageClassField;
            }
            set
            {
                this.voltageClassField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
        public int TestVoltage
        {
            get
            {
                return this.testVoltageField;
            }
            set
            {
                this.testVoltageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TestVoltageSpecified
        {
            get
            {
                return this.testVoltageFieldSpecified;
            }
            set
            {
                this.testVoltageFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
        public decimal Constant
        {
            get
            {
                return this.constantField;
            }
            set
            {
                this.constantField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ConstantSpecified
        {
            get
            {
                return this.constantFieldSpecified;
            }
            set
            {
                this.constantFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
        public string Phase
        {
            get
            {
                return this.phaseField;
            }
            set
            {
                this.phaseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
        public string Form
        {
            get
            {
                return this.formField;
            }
            set
            {
                this.formField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 14)]
        public string Base
        {
            get
            {
                return this.baseField;
            }
            set
            {
                this.baseField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
        public string RegisterRatio
        {
            get
            {
                return this.registerRatioField;
            }
            set
            {
                this.registerRatioField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
        public string AMIIndicator
        {
            get
            {
                return this.aMIIndicatorField;
            }
            set
            {
                this.aMIIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
        public string ERTIndicator
        {
            get
            {
                return this.eRTIndicatorField;
            }
            set
            {
                this.eRTIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 18)]
        public string TransformerRatedIndicator
        {
            get
            {
                return this.transformerRatedIndicatorField;
            }
            set
            {
                this.transformerRatedIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 19)]
        public string NetworkIndicator
        {
            get
            {
                return this.networkIndicatorField;
            }
            set
            {
                this.networkIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 20)]
        public string RemoteConnectDisconnectIndicator
        {
            get
            {
                return this.remoteConnectDisconnectIndicatorField;
            }
            set
            {
                this.remoteConnectDisconnectIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 21)]
        public string RecorderExists
        {
            get
            {
                return this.recorderExistsField;
            }
            set
            {
                this.recorderExistsField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 22)]
        public string LossCompensationCapableIndicator
        {
            get
            {
                return this.lossCompensationCapableIndicatorField;
            }
            set
            {
                this.lossCompensationCapableIndicatorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 23)]
        public string TestSequence
        {
            get
            {
                return this.testSequenceField;
            }
            set
            {
                this.testSequenceField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class DeviceClassificationCodeTypeTransformerAttribute
    {

        private decimal accuracyClassField;

        private bool accuracyClassFieldSpecified;

        private string constructionTypeField;

        private decimal basicLightingImpulseLevelField;

        private bool basicLightingImpulseLevelFieldSpecified;

        private decimal insulationVoltageClassField;

        private bool insulationVoltageClassFieldSpecified;

        private string insulatingMediumField;

        private string deviceApplicationEnvironmentField;

        private string numberOfRatiosField;

        private DeviceClassificationCodeTypeTransformerAttributeCurrentTransformer currentTransformerField;

        private DeviceClassificationCodeTypeTransformerAttributePotentialTransformer potentialTransformerField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public decimal AccuracyClass
        {
            get
            {
                return this.accuracyClassField;
            }
            set
            {
                this.accuracyClassField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccuracyClassSpecified
        {
            get
            {
                return this.accuracyClassFieldSpecified;
            }
            set
            {
                this.accuracyClassFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string ConstructionType
        {
            get
            {
                return this.constructionTypeField;
            }
            set
            {
                this.constructionTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public decimal BasicLightingImpulseLevel
        {
            get
            {
                return this.basicLightingImpulseLevelField;
            }
            set
            {
                this.basicLightingImpulseLevelField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BasicLightingImpulseLevelSpecified
        {
            get
            {
                return this.basicLightingImpulseLevelFieldSpecified;
            }
            set
            {
                this.basicLightingImpulseLevelFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public decimal InsulationVoltageClass
        {
            get
            {
                return this.insulationVoltageClassField;
            }
            set
            {
                this.insulationVoltageClassField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InsulationVoltageClassSpecified
        {
            get
            {
                return this.insulationVoltageClassFieldSpecified;
            }
            set
            {
                this.insulationVoltageClassFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string InsulatingMedium
        {
            get
            {
                return this.insulatingMediumField;
            }
            set
            {
                this.insulatingMediumField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string DeviceApplicationEnvironment
        {
            get
            {
                return this.deviceApplicationEnvironmentField;
            }
            set
            {
                this.deviceApplicationEnvironmentField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string NumberOfRatios
        {
            get
            {
                return this.numberOfRatiosField;
            }
            set
            {
                this.numberOfRatiosField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public DeviceClassificationCodeTypeTransformerAttributeCurrentTransformer CurrentTransformer
        {
            get
            {
                return this.currentTransformerField;
            }
            set
            {
                this.currentTransformerField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public DeviceClassificationCodeTypeTransformerAttributePotentialTransformer PotentialTransformer
        {
            get
            {
                return this.potentialTransformerField;
            }
            set
            {
                this.potentialTransformerField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class DeviceClassificationCodeTypeTransformerAttributeCurrentTransformer
    {

        private decimal lightLoadPercentageField;

        private bool lightLoadPercentageFieldSpecified;

        private string primaryCurrentRatio1Field;

        private string primaryCurrentRatio2Field;

        private string ohmsBurden1Field;

        private string ohmsBurden2Field;

        private decimal ratio1RatingFactorField;

        private bool ratio1RatingFactorFieldSpecified;

        private decimal ratio2RatingFactorField;

        private bool ratio2RatingFactorFieldSpecified;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public decimal LightLoadPercentage
        {
            get
            {
                return this.lightLoadPercentageField;
            }
            set
            {
                this.lightLoadPercentageField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LightLoadPercentageSpecified
        {
            get
            {
                return this.lightLoadPercentageFieldSpecified;
            }
            set
            {
                this.lightLoadPercentageFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string PrimaryCurrentRatio1
        {
            get
            {
                return this.primaryCurrentRatio1Field;
            }
            set
            {
                this.primaryCurrentRatio1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string PrimaryCurrentRatio2
        {
            get
            {
                return this.primaryCurrentRatio2Field;
            }
            set
            {
                this.primaryCurrentRatio2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string OhmsBurden1
        {
            get
            {
                return this.ohmsBurden1Field;
            }
            set
            {
                this.ohmsBurden1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string OhmsBurden2
        {
            get
            {
                return this.ohmsBurden2Field;
            }
            set
            {
                this.ohmsBurden2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public decimal Ratio1RatingFactor
        {
            get
            {
                return this.ratio1RatingFactorField;
            }
            set
            {
                this.ratio1RatingFactorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Ratio1RatingFactorSpecified
        {
            get
            {
                return this.ratio1RatingFactorFieldSpecified;
            }
            set
            {
                this.ratio1RatingFactorFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public decimal Ratio2RatingFactor
        {
            get
            {
                return this.ratio2RatingFactorField;
            }
            set
            {
                this.ratio2RatingFactorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool Ratio2RatingFactorSpecified
        {
            get
            {
                return this.ratio2RatingFactorFieldSpecified;
            }
            set
            {
                this.ratio2RatingFactorFieldSpecified = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1")]
    public partial class DeviceClassificationCodeTypeTransformerAttributePotentialTransformer
    {

        private string primaryVoltageRatio1Field;

        private string primaryVoltageRatio2Field;

        private string voltAmpsBurden1Field;

        private string voltAmpsBurden2Field;

        private string dualSecondaryField;

        private string fusedField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string PrimaryVoltageRatio1
        {
            get
            {
                return this.primaryVoltageRatio1Field;
            }
            set
            {
                this.primaryVoltageRatio1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string PrimaryVoltageRatio2
        {
            get
            {
                return this.primaryVoltageRatio2Field;
            }
            set
            {
                this.primaryVoltageRatio2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string VoltAmpsBurden1
        {
            get
            {
                return this.voltAmpsBurden1Field;
            }
            set
            {
                this.voltAmpsBurden1Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string VoltAmpsBurden2
        {
            get
            {
                return this.voltAmpsBurden2Field;
            }
            set
            {
                this.voltAmpsBurden2Field = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string DualSecondary
        {
            get
            {
                return this.dualSecondaryField;
            }
            set
            {
                this.dualSecondaryField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string Fused
        {
            get
            {
                return this.fusedField;
            }
            set
            {
                this.fusedField = value;
            }
        }
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped = false)]
    public partial class Update
    {

        [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://xmlns.alliantenergy.com/AssetManagement/DeviceClassificationCode/V1", Order = 0)]
        public UpdateDeviceClassificationCodeABMType UpdateDeviceClassificationCodeABM;

        public Update()
        {
        }

        public Update(UpdateDeviceClassificationCodeABMType UpdateDeviceClassificationCodeABM)
        {
            this.UpdateDeviceClassificationCodeABM = UpdateDeviceClassificationCodeABM;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DeviceClassificationCodeChannel : DeviceClassificationCode, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DeviceClassificationCodeClient : System.ServiceModel.ClientBase<DeviceClassificationCode>, DeviceClassificationCode
    {

        public DeviceClassificationCodeClient()
        {
        }

        public DeviceClassificationCodeClient(string endpointConfigurationName) :
            base(endpointConfigurationName)
        {
        }

        public DeviceClassificationCodeClient(string endpointConfigurationName, string remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DeviceClassificationCodeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
            base(endpointConfigurationName, remoteAddress)
        {
        }

        public DeviceClassificationCodeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void DeviceClassificationCode.Update(Update request)
        {
            base.Channel.Update(request);
        }

        public void Update(UpdateDeviceClassificationCodeABMType UpdateDeviceClassificationCodeABM)
        {
            Update inValue = new Update();
            inValue.UpdateDeviceClassificationCodeABM = UpdateDeviceClassificationCodeABM;
            ((DeviceClassificationCode)(this)).Update(inValue);
        }
    }
}
#pragma warning restore CS1591
