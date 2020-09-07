using System;
using ICities;
using Insights.Game.Events;
using Insights.Logging;
using UnityEngine;

namespace Insights.Game.Extensions
{
    public class BuildingExtension : BuildingExtensionBase
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(BuildingExtension));

        protected SessionContext Context { get; } = SessionContext.Instance;

        public override void OnBuildingCreated(ushort id)
        {
            Logger.LogDebug($"OnBuildingCreated > ID: {id}");

            var manager = BuildingManager.instance;
            var building = manager.m_buildings.m_buffer[id];

            // Create the Building Create event.
            var @event = new BuildingCreatedEvent
            {
                SessionTime = DateTimeOffset.Now,
                SessionId = Context.SessionId,
                GameTime = SimulationManager.instance.m_currentGameTime,
                EventType = Events.EventType.BuildingCreated,
                Id = id,
                Name = manager.GetBuildingName(id, InstanceID.Empty),
                ObjectName = building.Info.name,
                ClassName = building.Info.m_class.name
            };

            Logger.LogEvent(@event);

            base.OnBuildingCreated(id);
        }

        public override void OnBuildingReleased(ushort id)
        {
            Logger.LogDebug($"OnBuildingReleased > ID: {id}");

            var @event = new BuildingReleasedEvent
            {
                SessionTime = DateTimeOffset.Now,
                SessionId = Context.SessionId,
                GameTime = SimulationManager.instance.m_currentGameTime,
                EventType = Events.EventType.BuildingReleased,
                Id = id
            };

            Logger.LogEvent(@event);

            base.OnBuildingReleased(id);
        }

        public override void OnBuildingRelocated(ushort id)
        {
            Logger.LogDebug($"OnBuildingRelocated > ID: {id}");

            base.OnBuildingRelocated(id);
        }

        public override SpawnData OnCalculateSpawn(Vector3 location, SpawnData spawn)
        {
            Logger.LogDebug($"OnCalculateSpawn > Service: {spawn.service} | SubService: {spawn.subService} | Level: {spawn.level} | Style: {spawn.style}");

            return base.OnCalculateSpawn(location, spawn);
        }

        public override void OnCreated(IBuilding building)
        {
            Logger.LogDebug("OnCreated");

            base.OnCreated(building);
        }

        public override void OnReleased()
        {
            Logger.LogDebug("OnReleased");

            base.OnReleased();
        }
    }
}
