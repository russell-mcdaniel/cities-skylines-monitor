using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Insights.Utilities
{
    /// <summary>
    /// Provides XML serialization services.
    /// </summary>
    /// <remarks>
    /// If this class is changed to provide additional types to the XmlSerializer
    /// constructor, it must cache and reuse the serializer instance. See:
    /// 
    /// https://stackoverflow.com/questions/3326481/c-sharp-xml-serialization-of-derived-classes
    /// </remarks>
    public static class XmlHelper
    {
        public static string ToXml<T>(T entity)
        {
            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { OmitXmlDeclaration = true }))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    serializer.Serialize(writer, entity);
                }

                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static T FromXml<T>(string xml)
        {
            using (var stringReader = new StringReader(xml))
            using (var reader = XmlReader.Create(stringReader))
            {
                var serializer = new XmlSerializer(typeof(T));

                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
