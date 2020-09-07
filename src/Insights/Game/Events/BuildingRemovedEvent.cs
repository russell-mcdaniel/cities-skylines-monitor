using System.Xml.Serialization;

namespace Insights.Game.Events
{
    public class BuildingRemovedEvent : GameEvent
    {
        [XmlAttribute]
        public ushort Id { get; set; }
    }
}
