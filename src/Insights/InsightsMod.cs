using ICities;
using Insights.Game;
using Insights.Logging;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        protected InsightsLogger<InsightsMod> InsightsLogger { get; } = new InsightsLogger<InsightsMod>();

        private LoadingEvents _loadingEvents;

        #region IUserMod

        public string Name => "Insights";

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

            _loadingEvents = new LoadingEvents();
            _loadingEvents.Subscribe();
        }

        /// <summary>
        /// Called when the mod is disabled.
        /// </summary>
        /// <remarks>
        /// This method is not part of the IUserMod interface, but it is known by the game and called by convention.
        /// </remarks>
        public void OnDisabled()
        {
            _loadingEvents.Unsubscribe();

            InsightsLogger.Log("OnDisabled");
            InsightsLogger.Flush();
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

        private void InitializeEventSubscriptions()
        {
        }

        private void TerminateEventSubscriptions()
        {
        }
    }
}
