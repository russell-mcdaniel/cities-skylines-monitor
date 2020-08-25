using ICities;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class SerializableDataExtension : SerializableDataExtensionBase
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(SerializableDataExtension));

        public override void OnCreated(ISerializableData serializableData)
        {
            Logger.LogDebug($"OnCreated");

            base.OnCreated(serializableData);
        }

        public override void OnLoadData()
        {
            Logger.LogDebug($"OnLoadData");

            base.OnLoadData();
        }

        public override void OnReleased()
        {
            Logger.LogDebug($"OnReleased");

            base.OnReleased();
        }

        public override void OnSaveData()
        {
            Logger.LogDebug($"OnSaveData");

            base.OnSaveData();
        }
    }
}
