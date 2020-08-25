using ICities;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class LoadingExtension : LoadingExtensionBase
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(LoadingExtension));

        /// <summary>
        /// Occurs when one of the main game modes is entered.
        /// </summary>
        /// <remarks>
        /// This is called when entering one of the game modes from the main menu. This includes
        /// the game itself or one of the editors (asset, map, scenario, or theme).
        /// 
        /// It is not called from within an active session. For example, loading a different map
        /// while in the game does not call this method.
        /// </remarks>
        public override void OnCreated(ILoading loading)
        {
            Logger.LogDebug($"OnCreated > CurrentMode: {loading.currentMode} | CurrentTheme: {loading.currentTheme} | LoadingComplete: {loading.loadingComplete}");

            base.OnCreated(loading);
        }

        public override void OnLevelLoaded(LoadMode mode)
        {
            Logger.LogDebug($"OnLevelLoaded > LoadMode: {mode}");

            base.OnLevelLoaded(mode);
        }

        public override void OnLevelUnloading()
        {
            Logger.LogDebug("OnLevelUnloading");

            base.OnLevelUnloading();
        }

        public override void OnReleased()
        {
            Logger.LogDebug("OnReleased");

            base.OnReleased();
        }
    }
}
