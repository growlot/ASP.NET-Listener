//-----------------------------------------------------------------------
// <copyright file="DispatchByBodyElementOperationSelector.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.MessageBasedSoap
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel.Channels;
    using System.ServiceModel.Dispatcher;
    using System.Xml;

    /// <summary>
    /// Some SOAP 1.1 Web services stacks that do not follow the WS-I Basic Profile 1.1 guidelines do not dispatch messages based on the Action URI,
    /// but rather based on the XML qualified name of the first element inside the SOAP body. Likewise, the client side of these stacks might
    /// send messages with an empty or arbitrary HTTP SoapAction header, which was permitted by the SOAP 1.1 specification.
    /// This class selects operations based on the first element of the message body.
    /// <see href="http://msdn.microsoft.com/en-us/library/aa395223(v=vs.90).aspx">Initial source</see>
    /// </summary>
    public class DispatchByBodyElementOperationSelector : IDispatchOperationSelector
    {
        /// <summary>
        /// The dispatch dictionary.
        /// The class constructor expects a dictionary populated with pairs of XmlQualifiedName and strings, whereby
        /// the qualified names indicate the name of the first child of the SOAP body and the strings indicate the matching operation name
        /// </summary>
        private Dictionary<XmlQualifiedName, string> dispatchDictionary;

        /// <summary>
        /// The default operation name is the name of the operation that receives all messages that cannot be matched against this dictionary
        /// </summary>
        private string defaultOperationName;

        /// <summary>
        /// Initializes a new instance of the <see cref="DispatchByBodyElementOperationSelector"/> class.
        /// </summary>
        /// <param name="dispatchDictionary">The dispatch dictionary.</param>
        /// <param name="defaultOperationName">Default name of the operation.</param>
        public DispatchByBodyElementOperationSelector(Dictionary<XmlQualifiedName, string> dispatchDictionary, string defaultOperationName)
        {
            this.dispatchDictionary = dispatchDictionary;
            this.defaultOperationName = defaultOperationName;
        }

        /// <summary>
        /// Associates a local operation with the incoming method by inspecting an incoming message and returning a string that equals
        /// the name of a method on the service contract for the current endpoint.
        /// </summary>
        /// <param name="message">The incoming <see cref="T:System.ServiceModel.Channels.Message" /> to be associated with an operation.</param>
        /// <returns>
        /// The name of the operation to be associated with the <paramref name="message" />.
        /// </returns>
        public string SelectOperation(ref Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message", "Can not process empty message");
            }

            XmlDictionaryReader bodyReader = message.GetReaderAtBodyContents();
            XmlQualifiedName lookupQName = new
            XmlQualifiedName(bodyReader.LocalName, bodyReader.NamespaceURI);

            // Accessing the message body with GetReaderAtBodyContents or any of the other methods that provide access to the message's body
            // content causes the message to be marked as "read", which means that the message is invalid for any further processing.
            // Therefore we need to create a copy of the incoming message.
            message = CreateMessageCopy(message, bodyReader);

            if (this.dispatchDictionary.ContainsKey(lookupQName))
            {
                return this.dispatchDictionary[lookupQName];
            }
            else
            {
                return this.defaultOperationName;
            }
        }

        /// <summary>
        /// Creates the message copy.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="body">The message body reader.</param>
        /// <returns>The copy of the message</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "If put in Using scope message get's closed")]
        private static Message CreateMessageCopy(Message message, XmlDictionaryReader body)
        {
            Message copy = Message.CreateMessage(message.Version, message.Headers.Action, body);
            copy.Headers.CopyHeaderFrom(message, 0);
            copy.Properties.CopyProperties(message.Properties);
            return copy;
        }
    }
}
