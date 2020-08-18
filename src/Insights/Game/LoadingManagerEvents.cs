using Insights.Logging;

namespace Insights.Game
{
    public class LoadingManagerEvents
    {
        protected InsightsLogger<LoadingManagerEvents> InsightsLogger { get; } = new InsightsLogger<LoadingManagerEvents>();

        public void Subscribe()
        {
            var manager = LoadingManager.instance;

            manager.m_introLoaded += LoadingManagerIntroLoaded;
            manager.m_levelLoaded += LoadingManagerLevelLoaded;
            manager.m_levelPreLoaded += LoadingManagerLevelPreLoaded;
            manager.m_levelPreUnloaded += LoadingManagerLevelPreUnloaded;
            manager.m_levelUnloaded += LoadingManagerLevelUnloaded;
            manager.m_metaDataReady += LoadingManagerMetaDataReady;
            manager.m_simulationDataReady += LoadingManagerSimulationDataReady;
        }

        public void Unsubscribe()
        {
            var manager = LoadingManager.instance;

            manager.m_introLoaded -= LoadingManagerIntroLoaded;
            manager.m_levelLoaded -= LoadingManagerLevelLoaded;
            manager.m_levelPreLoaded -= LoadingManagerLevelPreLoaded;
            manager.m_levelPreUnloaded -= LoadingManagerLevelPreUnloaded;
            manager.m_levelUnloaded -= LoadingManagerLevelUnloaded;
            manager.m_metaDataReady -= LoadingManagerMetaDataReady;
            manager.m_simulationDataReady -= LoadingManagerSimulationDataReady;
        }

        private void LoadingManagerIntroLoaded()
        {
            InsightsLogger.Log("LoadingManagerIntroLoaded");
        }

        private void LoadingManagerLevelLoaded(SimulationManager.UpdateMode updateMode)
        {
            InsightsLogger.Log($"LoadingManagerLevelLoaded UpdateMode: {updateMode}");
        }

        private void LoadingManagerLevelPreLoaded()
        {
            InsightsLogger.Log("LoadingManagerLevelPreLoaded");
        }

        private void LoadingManagerLevelPreUnloaded()
        {
            InsightsLogger.Log("LoadingManagerLevelPreUnloaded");
        }

        private void LoadingManagerLevelUnloaded()
        {
            InsightsLogger.Log("LoadingManagerLevelUnloaded");
        }

        private void LoadingManagerMetaDataReady()
        {
            InsightsLogger.Log("LoadingManagerMetaDataReady");

            var manager = SimulationManager.instance;

            InsightsLogger.Log($"LoadingManagerMetaDataReady InstanceId: {manager?.m_metaData?.m_gameInstanceIdentifier} | CityName: {manager?.m_metaData?.m_CityName}");
        }

        private void LoadingManagerSimulationDataReady()
        {
            InsightsLogger.Log("LoadingManagerSimulationDataReady");
        }
    }
}
