//-----------------------------------------------------------------------
// <copyright file="MessageBasedSoapWebService.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace AMSLLC.Listener.Service.Implementation.MessageBasedSoap
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    /// <summary>
    /// Implements message based SOAP web service call.
    /// </summary>
    public static class MessageBasedSoapWebService
    {
        /// <summary>
        /// Makes a message based SOAP web service call.
        /// </summary>
        /// <typeparam name="T">The type of the SOAP message</typeparam>
        /// <param name="url">The URL.</param>
        /// <param name="soapMessage">The SOAP message.</param>
        public static void CallWebService<T>(Uri url, T soapMessage) where T : IXmlNamespaceExtension
        {
            HttpWebRequest webRequest = CreateWebRequest(url);

            string body = PrepareSoapRequest(soapMessage);
            InsertSoapEnvelopeIntoWebRequest(body, webRequest);            
            
            webRequest.GetResponse();
        }

        /// <summary>
        /// Prepares soap request from object.
        /// </summary>
        /// <typeparam name="T">The request object type. Must implement <see cref="IXmlNamespaceExtension"/></typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// Object serialized to string
        /// </returns>
        private static string PrepareSoapRequest<T>(T obj) where T : IXmlNamespaceExtension
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };

            StringBuilder xml = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(xml, settings))
            {
                writer.WriteStartElement("soapenv", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                writer.WriteStartElement("Header", "http://schemas.xmlsoap.org/soap/envelope/");
                writer.WriteEndElement(); // </Header>
                writer.WriteStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");

                var xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(writer, obj, obj.Namespaces);

                writer.WriteEndElement(); // </Body>
                writer.WriteEndElement(); // </Envelope>
            }

            return xml.ToString();
        }

        /// <summary>
        /// Creates the web request.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>The web request.</returns>
        private static HttpWebRequest CreateWebRequest(Uri url)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        /// <summary>
        /// Inserts the SOAP envelope into web request.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="webRequest">The web request.</param>
        private static void InsertSoapEnvelopeIntoWebRequest(string message, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                StreamWriter writer = new StreamWriter(stream);
                writer.Write(message);
                writer.Flush();
            }
        }
    }
}
