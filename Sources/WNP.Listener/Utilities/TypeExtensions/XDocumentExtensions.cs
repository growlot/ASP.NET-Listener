// //-----------------------------------------------------------------------
// <copyright file="XDocumentExtensions.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>
// //-----------------------------------------------------------------------

namespace System.Xml.Linq
{
    using System.Linq;
    using Collections.Generic;
    using Dynamic;

    /// <summary>
    /// Various methods to help serialize and deserialize objects to various formats
    /// </summary>
    public static class XDocumentExtensions
    {
        /// <summary>
        /// Convert <see cref="XDocument"/> to <see cref="ExpandoObject"/>
        /// </summary>
        /// <param name="document">The document.</param>
        /// <returns>The expando object.</returns>
        [Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "This is extension method, so this argument can't be null.")]
        public static dynamic AsExpando(this XDocument document)
        {
            var tmpDoc = new XDocument(new XElement("root"));
            tmpDoc.Root.Add();
            return CreateExpando(document.Root.Elements());
        }

        private static dynamic CreateExpando(IEnumerable<XElement> elements)
        {
            var returnValue = new ExpandoObject() as IDictionary<string, object>;

            foreach (var xElement in elements)
            {
                var elementValue = xElement.HasElements ? CreateExpando(xElement) : xElement.Value;
                returnValue.Add(xElement.Name.ToString(), elementValue);
            }

            return returnValue;
        }

        private static dynamic CreateExpando(XElement element)
        {
            var result = new ExpandoObject() as IDictionary<string, object>;

            if (element.Elements().Any(e => e.HasElements))
            {
                var list = new List<ExpandoObject>();

                result.Add(element.Name.ToString(), list);

                foreach (var childElement in element.Elements())
                {
                    list.Add(CreateExpando(childElement));
                }
            }
            else
            {
                foreach (var leafElement in element.Elements())
                {
                    result.Add(leafElement.Name.ToString(), leafElement.Value);
                }
            }

            return result;
        }
    }
}