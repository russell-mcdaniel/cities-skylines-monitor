using Insights.Logging;

namespace Insights.Game
{
    public class LoadingEvents
    {
        protected InsightsLogger<LoadingEvents> InsightsLogger { get; } = new InsightsLogger<LoadingEvents>();

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
        }

        private void LoadingManagerSimulationDataReady()
        {
            InsightsLogger.Log("LoadingManagerSimulationDataReady");
        }
    }
}
