using ICities;
using Insights.Logging;

namespace Insights.Game
{
    public class SerializableDataExtension : SerializableDataExtensionBase
    {
        protected InsightsLogger<SerializableDataExtension> InsightsLogger { get; } = new InsightsLogger<SerializableDataExtension>();

        public override void OnCreated(ISerializableData serializableData)
        {
            InsightsLogger.Log($"OnCreated");

            base.OnCreated(serializableData);
        }

        public override void OnLoadData()
        {
            InsightsLogger.Log($"OnLoadData");

            base.OnLoadData();
        }

        public override void OnReleased()
        {
            InsightsLogger.Log($"OnReleased");

            // Flush data at the end of a game session.
            InsightsLogger.Flush();

            base.OnReleased();
        }

        public override void OnSaveData()
        {
            InsightsLogger.Log($"OnSaveData");

            base.OnSaveData();
        }
    }
}
