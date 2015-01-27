//-----------------------------------------------------------------------
// <copyright file="AssetLoadServiceRequest.Customization.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.KCPL.Messages
{
    using System.Xml;
    using System.Xml.Serialization;
    using AMSLLC.Listener.Service.Implementation.MessageBasedSoap;

    /// <summary>
    /// Customization of auto-generated <see cref="AssetLoadServiceRequest"/> class
    /// </summary>
    public partial class AssetLoadServiceRequest : IXmlNamespaceExtension, IOdmRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetLoadServiceRequest"/> class.
        /// As noted below, per Microsoft's documentation, if the class exposes a public
        /// member of type XmlSerializerNamespaces decorated with the 
        /// XmlNamespacesDeclarationAttribute, then the XmlSerializer will utilize those
        /// namespaces during serialization.
        /// </summary>
        public AssetLoadServiceRequest()
        {
            this.Namespaces = new XmlSerializerNamespaces(new XmlQualifiedName[] 
            {
                new XmlQualifiedName(string.Empty, string.Empty) // Default Namespace. Second string should be actual namespace is it was set to something else than ""
                // Add any other namespaces, with prefixes, here.
            });
        }

        /// <summary>
        /// Gets the namespaces.
        /// Per Microsoft's documentation, you can add some public member that
        /// returns a XmlSerializerNamespaces object. Also, per the documentation, 
        /// for this to work with the XmlSerializer, decorate it with the 
        /// XmlNamespaceDeclarations attribute.
        /// </summary>
        /// <value>
        /// The namespaces.
        /// </value>
        [XmlIgnore]
        [XmlNamespaceDeclarations]
        public XmlSerializerNamespaces Namespaces { get; private set; }
    }

    /// <summary>
    /// Customization of auto-generated <see cref="AssetLoadServiceRequestAssetDetails"/> class
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Extends auto-generated file that contains multiple classes in it.")]
    public partial class AssetLoadServiceRequestAssetDetails
    {
        /// <summary>
        /// Gets a value indicating whether meterReceiptDate field is specified.
        /// Needed for <see cref="XmlSerializer"/> to omit field instead of using nil="true"
        /// </summary>
        /// <value>
        /// <c>true</c> if meterReceiptDate is specified; otherwise, <c>false</c>.
        /// </value>
        public bool meterReceiptDateSpecified
        {
            get
            {
                return this.meterReceiptDate != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether warrantyExpirationDate field is specified.
        /// Needed for <see cref="XmlSerializer"/> to omit field instead of using nil="true"
        /// </summary>
        /// <value>
        /// <c>true</c> if warrantyExpirationDate is specified; otherwise, <c>false</c>.
        /// </value>
        public bool warrantyExpirationDateSpecified
        {
            get
            {
                return this.warrantyExpirationDate != null;
            }
        }
    }
}
