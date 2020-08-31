using System.Xml.Serialization;

namespace Insights.Game.Events
{
    public class BuildingReleasedEvent : GameEvent
    {
        [XmlAttribute]
        public ushort Id { get; set; }
    }
}
