using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class LoadingManagerHandler
    {
        protected InsightsLogger<LoadingManagerHandler> Logger { get; } = new InsightsLogger<LoadingManagerHandler>();

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
            Logger.Log("IntroLoaded");
        }

        private void LevelLoaded(SimulationManager.UpdateMode updateMode)
        {
            Logger.Log($"LevelLoaded > UpdateMode: {updateMode}");
        }

        private void LevelPreLoaded()
        {
            Logger.Log("LevelPreLoaded");
        }

        private void LevelPreUnloaded()
        {
            Logger.Log("LevelPreUnloaded");
        }

        private void LevelUnloaded()
        {
            Logger.Log("LevelUnloaded");
        }

        private void MetaDataReady()
        {
            Logger.Log("MetaDataReady");

            var manager = SimulationManager.instance;

            Logger.Log($"MetaDataReady > SimulationManager > InstanceId: {manager?.m_metaData?.m_gameInstanceIdentifier} | CityName: {manager?.m_metaData?.m_CityName}");
        }

        private void SimulationDataReady()
        {
            Logger.Log("SimulationDataReady");
        }
    }
}
