using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMSLLC.Listener.Utilities
{
    using System.Dynamic;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public static class SerializationUtilities
    {
        public static string DictionaryToXml(Dictionary<string, object> dictionary)
        {
            XElement el = new XElement("root",
                dictionary.Select(kv => new XElement(kv.Key, kv.Value?.ToString())));
            return el.ToString(SaveOptions.DisableFormatting);
        }

        public static string AsString(this XmlDocument document)
        {
            using (var stringWriter = new StringWriter())
            using (var xmlTextWriter = XmlWriter.Create(stringWriter))
            {
                document.WriteTo(xmlTextWriter);
                xmlTextWriter.Flush();
                return stringWriter.GetStringBuilder().ToString();
            }
        }

        public static string ToXml<TModel>(TModel data)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(TModel));
            using (StringWriter sww = new StringWriter())
            using (XmlWriter writer = XmlWriter.Create(sww))
            {
                xsSubmit.Serialize(writer, data);
                return sww.ToString();
            }
        }

        public static string ToX<TModel>(TModel data)
        {
            XDocument target = new XDocument();
            XmlSerializer s = new XmlSerializer(typeof(TModel));
            using (XmlWriter writer = target.CreateWriter())
            {
                s.Serialize(writer, data);
            }
            return target.ToString();
        }

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
                returnValue.Add(xElement.Name.ToString(), xElement.HasElements ? CreateExpando(xElement) : xElement.Value);
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
                    list.Add(CreateExpando(childElement));
            }
            else
            {
                foreach (var leafElement in element.Elements())
                    result.Add(leafElement.Name.ToString(), leafElement.Value);
            }

            return result;
        }

        //public static XDocument JsonToXml(ExpandoObject expando)
        //{
        //    XDocument document = new XDocument();

        //    foreach (var o in expando)
        //    {
        //        document.
        //    }

        //    return document;
        //}
    }
}
