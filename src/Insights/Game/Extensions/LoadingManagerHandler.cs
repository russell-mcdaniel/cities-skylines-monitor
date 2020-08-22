using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class LoadingManagerHandler
    {
        protected InsightsLogger<LoadingManagerHandler> InsightsLogger { get; } = new InsightsLogger<LoadingManagerHandler>();

        public void Subscribe()
        {
            var manager = LoadingManager.instance;

            manager.m_introLoaded += IntroLoaded;
            manager.m_levelLoaded += LevelLoaded;
            manager.m_levelPreLoaded += LevelPreLoaded;
            manager.m_levelPreUnloaded += LevelPreUnloaded;
            manager.m_levelUnloaded += LevelUnloaded;
            manager.m_metaDataReady += MetaDataReady;
            manager.m_simulationDataReady += SimulationDataReady;
        }

        public void Unsubscribe()
        {
            var manager = LoadingManager.instance;

            manager.m_introLoaded -= IntroLoaded;
            manager.m_levelLoaded -= LevelLoaded;
            manager.m_levelPreLoaded -= LevelPreLoaded;
            manager.m_levelPreUnloaded -= LevelPreUnloaded;
            manager.m_levelUnloaded -= LevelUnloaded;
            manager.m_metaDataReady -= MetaDataReady;
            manager.m_simulationDataReady -= SimulationDataReady;
        }

        private void IntroLoaded()
        {
            InsightsLogger.Log("IntroLoaded");
        }

        private void LevelLoaded(SimulationManager.UpdateMode updateMode)
        {
            InsightsLogger.Log($"LevelLoaded > UpdateMode: {updateMode}");
        }

        private void LevelPreLoaded()
        {
            InsightsLogger.Log("LevelPreLoaded");
        }

        private void LevelPreUnloaded()
        {
            InsightsLogger.Log("LevelPreUnloaded");
        }

        private void LevelUnloaded()
        {
            InsightsLogger.Log("LevelUnloaded");
        }

        private void MetaDataReady()
        {
            InsightsLogger.Log("MetaDataReady");

            var manager = SimulationManager.instance;

            InsightsLogger.Log($"MetaDataReady > SimulationManager > InstanceId: {manager?.m_metaData?.m_gameInstanceIdentifier} | CityName: {manager?.m_metaData?.m_CityName}");
        }

        private void SimulationDataReady()
        {
            InsightsLogger.Log("SimulationDataReady");
        }
    }
}
