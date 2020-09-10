using System;
using System.Reflection;
using ICities;
using Insights.Game.Extensions;
using Insights.Logging;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(InsightsMod));

        private readonly string _version = typeof(InsightsMod).Assembly.GetName().Version.ToString();

        private InsightsUploader _uploader;

        private BuildingManagerHandler _buildingManagerHandler;
        private LoadingManagerHandler _loadingManagerHandler;
        private LocaleManagerHandler _localeManagerHandler;
        private PlatformServiceHandler _platformServiceHandler;
        private PluginManagerHandler _pluginManagerHandler;
        private SceneManagerHandler _sceneManagerHandler;

        #region IUserMod

        public string Name => $"Insights {_version}";

        public string Description => "Gain insights from gameplay";

        #endregion

        public InsightsMod()
        {
            Logger.LogDebug($"{nameof(InsightsMod)} > Instantiating...");

            // Create uploader.
            _uploader = new InsightsUploader();

            Logger.LogDebug($"{nameof(InsightsMod)} > Instantiated.");
        }

        /// <summary>
        /// Called when the mod is enabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnEnabled()
        {
            Logger.LogDebug($"{nameof(OnEnabled)} > Enabling...");

            // Log the Mono runtime version.
            var monoRuntime = Type.GetType("Mono.Runtime");

            if (monoRuntime != null)
            {
                var displayName = monoRuntime.GetMethod(
                    "GetDisplayName",
                    BindingFlags.NonPublic | BindingFlags.Static);

                if (displayName != null)
                {
                    Logger.LogInfo($"OnEnabled > The Mono runtime version is {displayName.Invoke(null, null)}.");
                }
            }

            // Subscribe to mod events.
            Subscribe();

            // Start uploader.
            _uploader.Start();

            Logger.LogDebug($"{nameof(OnEnabled)} > Enabled.");
        }

        /// <summary>
        /// Called when the mod is disabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnDisabled()
        {
            Logger.LogDebug($"{nameof(OnDisabled)} > Disabling...");

            // Unsubscribe from mod events.
            Unsubscribe();

            // Stop uploader.
            _uploader.Stop();

            Logger.LogDebug($"{nameof(OnDisabled)} > Disabled.");
            Logger.Reset();
        }

        /// <summary>
        /// Called when the mod configuration user interface is invoked.
        /// </summary>
        /// <param name="helper"></param>
        //public void OnSettingsUI(UIHelperBase helper)
        //{
        //    // TODO: Implement settings for user key and secret.
        //    // https://skylines.paradoxwikis.com/Mod_Options_Panel
        //}

        private void Subscribe()
        {
            _buildingManagerHandler = new BuildingManagerHandler();
            _buildingManagerHandler.Subscribe();

            _loadingManagerHandler = new LoadingManagerHandler();
            _loadingManagerHandler.Subscribe();

            _localeManagerHandler = new LocaleManagerHandler();
            _localeManagerHandler.Subscribe();

            _platformServiceHandler = new PlatformServiceHandler();
            _platformServiceHandler.Subscribe();

            _pluginManagerHandler = new PluginManagerHandler();
            _pluginManagerHandler.Subscribe();

            _sceneManagerHandler = new SceneManagerHandler();
            _sceneManagerHandler.Subscribe();
        }

        private void Unsubscribe()
        {
            _buildingManagerHandler.Unsubscribe();
            _loadingManagerHandler.Unsubscribe();
            _localeManagerHandler.Subscribe();
            _platformServiceHandler.Unsubscribe();
            _pluginManagerHandler.Unsubscribe();
            _sceneManagerHandler.Unsubscribe();
        }
    }
}
