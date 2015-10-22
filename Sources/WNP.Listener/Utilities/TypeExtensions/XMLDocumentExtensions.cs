// //-----------------------------------------------------------------------
// <copyright file="XMLDocumentExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace System.Xml
{
    using Globalization;
    using IO;

    /// <summary>
    /// Various methods to help serialize and deserialize objects to various formats
    /// </summary>
    public static class XMLDocumentExtensions
    {
        /// <summary>
        /// Convert <see cref="XmlDocument"/> to XML string
        /// </summary>
        /// <param name="document">The XML document.</param>
        /// <returns>The XML string</returns>
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1059:MembersShouldNotExposeCertainConcreteTypes", MessageId = "System.Xml.XmlNode", Justification = "This is extension method for concrete type. Can't use other type.")]
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static string AsString(this XmlDocument document)
        {
            StringWriter stringWriter = null;
            try
            {
                stringWriter = new StringWriter(CultureInfo.InvariantCulture);

                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    stringWriter = null;
                    document.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                }

                return stringWriter.GetStringBuilder().ToString();
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