using System;
using System.Globalization;
using System.Xml.Serialization;

namespace Insights.Game.Events
{
    [XmlInclude(typeof(SessionStartedEvent))]
    [XmlInclude(typeof(SessionEndedEvent))]
    [XmlInclude(typeof(BuildingCreatedEvent))]
    [XmlInclude(typeof(BuildingRemovedEvent))]
    public abstract class GameEvent
    {
        /// <summary>
        /// Gets or sets the session time for the event (the real-world time).
        /// </summary>
        [XmlIgnore]
        public DateTimeOffset SessionTime { get; set; }

        /// <summary>
        /// Gets or sets the session time value for the XmlSerializer. Clients should not use this property.
        /// </summary>
        /// <remarks>
        /// This is a workaround for a limitation of the XmlSerializer. It cannot encode or decode
        /// complex types to/from attributes. To avoid converting the property to an element, a string
        /// representation of the property is maintained for serialization purposes.
        /// </remarks>
        [XmlAttribute(AttributeName = "SessionTime")]
        public string SessionTimeXml
        {
            get { return SessionTime.ToString("O"); }
            set { SessionTime = DateTimeOffset.Parse(value, CultureInfo.InvariantCulture); }
        }

        [XmlAttribute]
        public Guid SessionId { get; set; }

        /// <summary>
        /// Gets or sets the game time for the event (the in-game simulated time).
        /// </summary>
        [XmlIgnore]
        public DateTime GameTime { get; set; }

        /// <summary>
        /// Gets or sets the game time value for the XmlSerializer. Clients should not use this property.
        /// </summary>
        /// <remarks>
        /// This is a workaround for a limitation of the XmlSerializer. It cannot encode or decode
        /// complex types to/from attributes. To avoid converting the property to an element, a string
        /// representation of the property is maintained for serialization purposes.
        /// </remarks>
        [XmlAttribute(AttributeName = "GameTime")]
        public string GameTimeXml
        {
            get { return GameTime.ToString("O"); }
            set { GameTime = DateTime.Parse(value, CultureInfo.InvariantCulture);  }
        }

        [XmlAttribute]
        public EventType EventType { get; set; }
    }
}
