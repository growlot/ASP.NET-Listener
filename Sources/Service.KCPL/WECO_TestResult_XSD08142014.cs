﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.18020.
// 
namespace Service.KCPL {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute("W1-CreateActivityInboundComm", Namespace="", IsNullable=false)]
    public partial class W1CreateActivityInboundComm1 {
        
        private string externalSystemField;
        
        private string externalReferenceIdField;
        
        private string externalPkValue1Field;
        
        private W1CreateActivityInboundCommRawMessage[] rawMessageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string externalSystem {
            get {
                return this.externalSystemField;
            }
            set {
                this.externalSystemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string externalReferenceId {
            get {
                return this.externalReferenceIdField;
            }
            set {
                this.externalReferenceIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string externalPkValue1 {
            get {
                return this.externalPkValue1Field;
            }
            set {
                this.externalPkValue1Field = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("rawMessage", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public W1CreateActivityInboundCommRawMessage[] rawMessage {
            get {
                return this.rawMessageField;
            }
            set {
                this.rawMessageField = value;
            }
        }
    }
    
    /// <remarks/>
    public partial class W1CreateActivityInboundCommRawMessage {
        
        private string badgeNoField1;
        
        private System.DateTime testDateTimeField;
        
        private bool testDateTimeFieldSpecified;
        
        private string testTypeField;
        
        private string testerIdField;
        
        private W1CreateActivityInboundCommRawMessageTestResults[] testResultsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string badgeNo1 {
            get {
                return this.badgeNoField1;
            }
            set {
                this.badgeNoField1 = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public System.DateTime testDateTime {
            get {
                return this.testDateTimeField;
            }
            set {
                this.testDateTimeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool testDateTimeSpecified {
            get {
                return this.testDateTimeFieldSpecified;
            }
            set {
                this.testDateTimeFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string testType {
            get {
                return this.testTypeField;
            }
            set {
                this.testTypeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string testerId {
            get {
                return this.testerIdField;
            }
            set {
                this.testerIdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("testResults", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public W1CreateActivityInboundCommRawMessageTestResults[] testResults {
            get {
                return this.testResultsField;
            }
            set {
                this.testResultsField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class W1CreateActivityInboundCommRawMessageTestResults {
        
        private string seriesPowerFactorField;
        
        private string testLocationField;
        
        private W1CreateActivityInboundCommRawMessageTestResultsAsFound[] asFoundField;
        
        private W1CreateActivityInboundCommRawMessageTestResultsAsLeft[] asLeftField;
        
        private W1CreateActivityInboundCommRawMessageTestResultsMeterReadsListMeterReads[][] meterReadsListField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string seriesPowerFactor {
            get {
                return this.seriesPowerFactorField;
            }
            set {
                this.seriesPowerFactorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string testLocation {
            get {
                return this.testLocationField;
            }
            set {
                this.testLocationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("asFound", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public W1CreateActivityInboundCommRawMessageTestResultsAsFound[] asFound {
            get {
                return this.asFoundField;
            }
            set {
                this.asFoundField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("asLeft", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public W1CreateActivityInboundCommRawMessageTestResultsAsLeft[] asLeft {
            get {
                return this.asLeftField;
            }
            set {
                this.asLeftField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("meterReads", typeof(W1CreateActivityInboundCommRawMessageTestResultsMeterReadsListMeterReads), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable=false)]
        public W1CreateActivityInboundCommRawMessageTestResultsMeterReadsListMeterReads[][] meterReadsList {
            get {
                return this.meterReadsListField;
            }
            set {
                this.meterReadsListField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class W1CreateActivityInboundCommRawMessageTestResultsAsFound {
        
        private string fullLoadField;
        
        private string lightLoadField;
        
        private string weightedAverageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fullLoad {
            get {
                return this.fullLoadField;
            }
            set {
                this.fullLoadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lightLoad {
            get {
                return this.lightLoadField;
            }
            set {
                this.lightLoadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string weightedAverage {
            get {
                return this.weightedAverageField;
            }
            set {
                this.weightedAverageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class W1CreateActivityInboundCommRawMessageTestResultsAsLeft {
        
        private string fullLoadField;
        
        private string lightLoadField;
        
        private string weightedAverageField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fullLoad {
            get {
                return this.fullLoadField;
            }
            set {
                this.fullLoadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string lightLoad {
            get {
                return this.lightLoadField;
            }
            set {
                this.lightLoadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string weightedAverage {
            get {
                return this.weightedAverageField;
            }
            set {
                this.weightedAverageField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    public partial class W1CreateActivityInboundCommRawMessageTestResultsMeterReadsListMeterReads {
        
        private string channelField;
        
        private string readingField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string channel {
            get {
                return this.channelField;
            }
            set {
                this.channelField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string reading {
            get {
                return this.readingField;
            }
            set {
                this.readingField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
    public partial class NewDataSet2 {
        
        private W1CreateActivityInboundComm1[] itemsField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("W1-CreateActivityInboundComm")]
        public W1CreateActivityInboundComm1[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
    }
}
