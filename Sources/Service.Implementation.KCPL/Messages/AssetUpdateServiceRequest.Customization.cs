//-----------------------------------------------------------------------
// <copyright file="AssetUpdateServiceRequest.Customization.cs" company="Advanced Metering Services LLC">
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
    public partial class AssetUpdateServiceRequest : IXmlNamespaceExtension, IOdmRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetUpdateServiceRequest"/> class.
        /// As noted below, per Microsoft's documentation, if the class exposes a public
        /// member of type XmlSerializerNamespaces decorated with the 
        /// XmlNamespacesDeclarationAttribute, then the XmlSerializer will utilize those
        /// namespaces during serialization.
        /// </summary>
        public AssetUpdateServiceRequest()
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
}