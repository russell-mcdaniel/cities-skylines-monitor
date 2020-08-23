using ICities;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class SerializableDataExtension : SerializableDataExtensionBase
    {
        protected InsightsLogger<SerializableDataExtension> Logger { get; } = new InsightsLogger<SerializableDataExtension>();

        public override void OnCreated(ISerializableData serializableData)
        {
            Logger.Log($"OnCreated");

            base.OnCreated(serializableData);
        }

        public override void OnLoadData()
        {
            Logger.Log($"OnLoadData");

            base.OnLoadData();
        }

        public override void OnReleased()
        {
            Logger.Log($"OnReleased");

            // Flush data at the end of a game session.
            Logger.Flush();

            base.OnReleased();
        }

        public override void OnSaveData()
        {
            Logger.Log($"OnSaveData");

            base.OnSaveData();
        }
    }
}
