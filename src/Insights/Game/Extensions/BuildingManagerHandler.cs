using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class BuildingManagerHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(BuildingManagerHandler));

        public void Subscribe()
        {
            var manager = BuildingManager.instance;

            manager.EventBuildingCreated += BuildingCreated;
            manager.EventBuildingReleased += BuildingReleased;
            manager.EventBuildingRelocated += BuildingRelocated;
        }

        public void Unsubscribe()
        {
            var manager = BuildingManager.instance;

            manager.EventBuildingCreated -= BuildingCreated;
            manager.EventBuildingReleased -= BuildingReleased;
            manager.EventBuildingRelocated -= BuildingRelocated;
        }

        private void BuildingCreated(ushort building)
        {
            Logger.LogDebug($"BuildingCreated > ID: {building}");
        }

        private void BuildingReleased(ushort building)
        {
            Logger.LogDebug($"BuildingReleased > ID: {building}");
        }

        private void BuildingRelocated(ushort building)
        {
            Logger.LogDebug($"BuildingRelocated > ID: {building}");
        }
    }
}
