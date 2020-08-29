using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Insights.Game.Events
{
    [XmlInclude(typeof(SessionBeginEvent))]
    public abstract class GameEvent
    {
        /// <summary>
        /// Gets or sets the timestamp for the event.
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the timestamp value for the XmlSerializer. Clients should not use this property.
        /// </summary>
        /// <remarks>
        /// This is a workaround for a limitation of the XmlSerializer. It cannot encode or decode
        /// complex types to/from attributes. To avoid converting the property to an element, a string
        /// representation of the property is maintained for serialization purposes.
        /// </remarks>
        [XmlAttribute(AttributeName = "Timestamp")]
        public string TimestampXml
        {
            get { return Timestamp.ToString("O"); }
            set { Timestamp = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture); }
        }

        [XmlAttribute]
        public EventType EventType { get; set; }

        [XmlAttribute]
        public Guid SessionId { get; set; }
    }
}
