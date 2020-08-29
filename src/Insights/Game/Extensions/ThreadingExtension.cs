using ICities;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    /// <remarks>
    /// Omits several overrides for performance and efficiency. These have high-frequency
    /// calls that will flood the log and cause performance problems.
    /// </remarks>
    public class ThreadingExtension : ThreadingExtensionBase
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(ThreadingExtension));

        //public override void OnAfterSimulationFrame()
        //{
        //    Logger.LogDebug($"OnAfterSimulationFrame");
        //
        //    base.OnAfterSimulationFrame();
        //}

        //public override void OnAfterSimulationTick()
        //{
        //    Logger.LogDebug($"OnAfterSimulationTick");
        //
        //    base.OnAfterSimulationTick();
        //}

        //public override void OnBeforeSimulationFrame()
        //{
        //    Logger.LogDebug($"OnBeforeSimulationFrame");
        //
        //    base.OnBeforeSimulationFrame();
        //}

        //public override void OnBeforeSimulationTick()
        //{
        //    Logger.LogDebug($"OnBeforeSimulationTick");
        //
        //    base.OnBeforeSimulationTick();
        //}

        public override void OnCreated(IThreading threading)
        {
            // These values are available from the IThreading instance available on this class if there
            // is a need to log their values on an ongoing basis (see "threadingManager" member).
            Logger.LogDebug($"OnCreated > TimePerFrame: {threading.timePerFrame} | RenderTime: {threading.renderTime} | RenderDayTimeHour: {threading.renderDayTimeHour} | SimTime: {threading.simulationTime} | SimDayTimeHour: {threading.simulationDayTimeHour} | SimNightTime: {threading.simulationNightTime} | SimSpeed: {threading.simulationSpeed} | SimPaused: {threading.simulationPaused}");

            base.OnCreated(threading);
        }

        public override void OnReleased()
        {
            Logger.LogDebug($"OnReleased");

            base.OnReleased();
        }

        //public override void OnUpdate(float realTimeDelta, float simulationTimeDelta)
        //{
        //    Logger.LogDebug($"OnUpdate > RealTimeDelta: {realTimeDelta} | SimulationTimeDelta: {simulationTimeDelta}");
        //
        //    base.OnUpdate(realTimeDelta, simulationTimeDelta);
        //}
    }
}
