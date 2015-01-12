//-----------------------------------------------------------------------
// <copyright file="IXmlNamespaceExtension.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.MessageBasedSoap
{
    using System.Xml.Serialization;

    /// <summary>
    /// Interface that allows usage of extended request classes in generic function.
    /// </summary>
    public interface IXmlNamespaceExtension
    {
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
        XmlSerializerNamespaces Namespaces { get; }
    }
}
