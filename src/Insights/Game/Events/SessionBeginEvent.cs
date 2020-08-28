using System;
using System.Xml.Serialization;
using ICities;

namespace Insights.Game.Events
{
    public class SessionBeginEvent : GameEvent
    {
        [XmlAttribute]
        public AppMode Type { get; set; }

        [XmlAttribute]
        public SimulationManager.UpdateMode Subtype { get; set; }

        [XmlAttribute]
        public Guid InstanceId { get; set; }

        [XmlAttribute]
        public string CityName { get; set; }
    }
}
