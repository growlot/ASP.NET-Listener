﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
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
    public partial class AssetUpdateServiceRequest
    {

        private int listenerTransactionIdField;

        private string badgeNoField;

        private System.DateTime statusDateTimeField;

        private AssetUpdateServiceRequestStatus statusField;

        private string retirementReasonCodeField;

        /// <remarks/>
        public int listenerTransactionId
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
        public System.DateTime statusDateTime
        {
            get
            {
                return this.statusDateTimeField;
            }
            set
            {
                this.statusDateTimeField = value;
            }
        }

        /// <remarks/>
        public AssetUpdateServiceRequestStatus status
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
        public string retirementReasonCode
        {
            get
            {
                return this.retirementReasonCodeField;
            }
            set
            {
                this.retirementReasonCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public enum AssetUpdateServiceRequestStatus
    {

        /// <remarks/>
        RETIRED,

        /// <remarks/>
        ACTIVE,

        /// <remarks/>
        REPAIR,
    }
}
