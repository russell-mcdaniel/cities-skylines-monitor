using ICities;
using Insights.Game.Extensions;
using Insights.Logging;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        protected InsightsLogger<InsightsMod> InsightsLogger { get; } = new InsightsLogger<InsightsMod>();

        private readonly string _version = typeof(InsightsMod).Assembly.GetName().Version.ToString();

        private LoadingManagerHandler _loadingManagerHandler;
        private PluginManagerHandler _pluginManagerHandler;

        #region IUserMod

        public string Name => $"Insights {_version}";

        public string Description => "Gain insights from gameplay";

        #endregion

        public InsightsMod()
        {
            InsightsLogger.Log("Instantiated");
        }

        /// <summary>
        /// Called when the mod is enabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnEnabled()
        {
            InsightsLogger.Log("OnEnabled");

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

            InsightsLogger.Log("OnDisabled");
            InsightsLogger.Reset();
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

            _pluginManagerHandler = new PluginManagerHandler();
            _pluginManagerHandler.Subscribe();
        }

        private void Unsubscribe()
        {
            _pluginManagerHandler.Unsubscribe();
            _loadingManagerHandler.Unsubscribe();
        }
    }
}
