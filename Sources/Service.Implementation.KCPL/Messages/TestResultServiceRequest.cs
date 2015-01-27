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
namespace AMSLLC.Listener.Service.Implementation.KCPL.Messages
{
    using System.Xml.Serialization;


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class TestResultServiceRequest
    {

        private string listenerTransactionIdField;

        private string badgeNoField;

        private System.DateTime testDateTimeField;

        private string testerIdField;

        private TestResultServiceRequestTestType testTypeField;

        private TestResultServiceRequestTestLocation testLocationField;

        private TestResultServiceRequestTestResults testResultsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string listenerTransactionId
        {
            get
            {
                return this.listenerTransactionIdField;
            }
            set
            {
                this.listenerTransactionIdField = value;
            }
        }

        /// <remarks/>
        public string badgeNo
        {
            get
            {
                return this.badgeNoField;
            }
            set
            {
                this.badgeNoField = value;
            }
        }

        /// <remarks/>
        public System.DateTime testDateTime
        {
            get
            {
                return this.testDateTimeField;
            }
            set
            {
                this.testDateTimeField = value;
            }
        }

        /// <remarks/>
        public string testerId
        {
            get
            {
                return this.testerIdField;
            }
            set
            {
                this.testerIdField = value;
            }
        }

        /// <remarks/>
        public TestResultServiceRequestTestType testType
        {
            get
            {
                return this.testTypeField;
            }
            set
            {
                this.testTypeField = value;
            }
        }

        /// <remarks/>
        public TestResultServiceRequestTestLocation testLocation
        {
            get
            {
                return this.testLocationField;
            }
            set
            {
                this.testLocationField = value;
            }
        }

        /// <remarks/>
        public TestResultServiceRequestTestResults testResults
        {
            get
            {
                return this.testResultsField;
            }
            set
            {
                this.testResultsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum TestResultServiceRequestTestType
    {

        /// <remarks/>
        NS,

        /// <remarks/>
        NT,

        /// <remarks/>
        SS,

        /// <remarks/>
        MT,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum TestResultServiceRequestTestLocation
    {

        /// <remarks/>
        FL,

        /// <remarks/>
        MN,

        /// <remarks/>
        SH,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TestResultServiceRequestTestResults
    {

        private string seriesPowerFactorField;

        private TestResultServiceRequestTestResultsMeterReads[] meterReadsListField;

        private TestResultServiceRequestTestResultsAsFound asFoundField;

        private TestResultServiceRequestTestResultsAsLeft asLeftField;

        /// <remarks/>
        public string seriesPowerFactor
        {
            get
            {
                return this.seriesPowerFactorField;
            }
            set
            {
                this.seriesPowerFactorField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("meterReads", IsNullable = false)]
        public TestResultServiceRequestTestResultsMeterReads[] meterReadsList
        {
            get
            {
                return this.meterReadsListField;
            }
            set
            {
                this.meterReadsListField = value;
            }
        }

        /// <remarks/>
        public TestResultServiceRequestTestResultsAsFound asFound
        {
            get
            {
                return this.asFoundField;
            }
            set
            {
                this.asFoundField = value;
            }
        }

        /// <remarks/>
        public TestResultServiceRequestTestResultsAsLeft asLeft
        {
            get
            {
                return this.asLeftField;
            }
            set
            {
                this.asLeftField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TestResultServiceRequestTestResultsMeterReads
    {

        private string channelField;

        private string readingField;

        /// <remarks/>
        public string channel
        {
            get
            {
                return this.channelField;
            }
            set
            {
                this.channelField = value;
            }
        }

        /// <remarks/>
        public string reading
        {
            get
            {
                return this.readingField;
            }
            set
            {
                this.readingField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TestResultServiceRequestTestResultsAsFound
    {

        private string fullLoadField;

        private string lightLoadField;

        private string weightedAverageField;

        /// <remarks/>
        public string fullLoad
        {
            get
            {
                return this.fullLoadField;
            }
            set
            {
                this.fullLoadField = value;
            }
        }

        /// <remarks/>
        public string lightLoad
        {
            get
            {
                return this.lightLoadField;
            }
            set
            {
                this.lightLoadField = value;
            }
        }

        /// <remarks/>
        public string weightedAverage
        {
            get
            {
                return this.weightedAverageField;
            }
            set
            {
                this.weightedAverageField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class TestResultServiceRequestTestResultsAsLeft
    {

        private string fullLoadField;

        private string lightLoadField;

        private string weightedAverageField;

        /// <remarks/>
        public string fullLoad
        {
            get
            {
                return this.fullLoadField;
            }
            set
            {
                this.fullLoadField = value;
            }
        }

        /// <remarks/>
        public string lightLoad
        {
            get
            {
                return this.lightLoadField;
            }
            set
            {
                this.lightLoadField = value;
            }
        }

        /// <remarks/>
        public string weightedAverage
        {
            get
            {
                return this.weightedAverageField;
            }
            set
            {
                this.weightedAverageField = value;
            }
        }
    }
}