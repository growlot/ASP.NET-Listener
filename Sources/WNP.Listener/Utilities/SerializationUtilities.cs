// //-----------------------------------------------------------------------
// <copyright file="SerializationUtilities.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace AMSLLC.Listener.Utilities
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Various methods to help serialize and deserialize objects to various formats
    /// </summary>
    public static class SerializationUtilities
    {
        /// <summary>
        /// Converts name value dictionary to XML string.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <returns>The XML string</returns>
        public static string DictionaryToXml(Dictionary<string, object> dictionary)
        {
            XElement el = new XElement("root", dictionary.Select(kv => new XElement(kv.Key, kv.Value?.ToString())));
            return el.ToString(SaveOptions.DisableFormatting);
        }

        /// <summary>
        /// Convert any type to XML string.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="data">The data.</param>
        /// <returns>The XML string</returns>
        public static string ToXml<TModel>(TModel data)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TModel));
            StringWriter stringWriter = null;
            try
            {
                stringWriter = new StringWriter(CultureInfo.InvariantCulture);

                using (XmlWriter writer = XmlWriter.Create(stringWriter))
                {
                    stringWriter = null;
                    xsSubmit.Serialize(writer, data);
                }

                return stringWriter.ToString();
            }
            finally
            {
                if (stringWriter != null)
                {
                    stringWriter.Dispose();
                }
            }
        }
    }
}