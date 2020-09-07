using System.Xml.Serialization;

namespace Insights.Game.Events
{
    public class BuildingCreatedEvent : GameEvent
    {
        [XmlAttribute]
        public ushort Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string ObjectName { get; set; }

        [XmlAttribute]
        public string ClassName { get; set; }
    }
}
