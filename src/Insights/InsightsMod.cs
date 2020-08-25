using ICities;
using Insights.Game.Extensions;
using Insights.Logging;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(InsightsMod));

        private readonly string _version = typeof(InsightsMod).Assembly.GetName().Version.ToString();

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
            Logger.LogDebug("Instantiated");
        }

        /// <summary>
        /// Called when the mod is enabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnEnabled()
        {
            Logger.LogDebug("OnEnabled");

            Subscribe();
        }

        /// <summary>
        /// Called when the mod is disabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnDisabled()
        {
            Unsubscribe();

            Logger.LogDebug("OnDisabled");
            Logger.Reset();
        }

        /// <summary>
        /// Called when 
        /// </summary>
        /// <param name="helper"></param>
        //public void OnSettingsUI(UIHelperBase helper)
        //{
        //    // TODO: Implement settings for user key and secret.
        //    // https://skylines.paradoxwikis.com/Mod_Options_Panel
        //}

        private void Subscribe()
        {
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
            _loadingManagerHandler.Unsubscribe();
            _localeManagerHandler.Subscribe();
            _platformServiceHandler.Unsubscribe();
            _pluginManagerHandler.Unsubscribe();
            _sceneManagerHandler.Unsubscribe();
        }
    }
}
