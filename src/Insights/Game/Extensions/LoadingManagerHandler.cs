using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class LoadingManagerHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(LoadingManagerHandler));

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
            Logger.LogDebug("IntroLoaded");
        }

        private void LevelLoaded(SimulationManager.UpdateMode updateMode)
        {
            Logger.LogDebug($"LevelLoaded > UpdateMode: {updateMode}");
        }

        private void LevelPreLoaded()
        {
            Logger.LogDebug("LevelPreLoaded");
        }

        private void LevelPreUnloaded()
        {
            Logger.LogDebug("LevelPreUnloaded");
        }

        private void LevelUnloaded()
        {
            Logger.LogDebug("LevelUnloaded");
        }

        private void MetaDataReady()
        {
            Logger.LogDebug("MetaDataReady");

            var manager = SimulationManager.instance;

            Logger.LogDebug($"MetaDataReady > SimulationManager > InstanceId: {manager?.m_metaData?.m_gameInstanceIdentifier} | CityName: {manager?.m_metaData?.m_CityName}");
        }

        private void SimulationDataReady()
        {
            Logger.LogDebug("SimulationDataReady");
        }
    }
}
