using ColossalFramework.Globalization;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class LocaleManagerHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(LocaleManagerHandler));

        public void Subscribe()
        {
            LocaleManager.eventLocaleChanged += LocaleChanged;
            LocaleManager.eventLocaleNeedsSubsitution += LocaleNeedsSubsitution;
            LocaleManager.eventUIComponentLocaleChanged += UIComponentLocaleChanged;
        }

        public void Unsubscribe()
        {
            LocaleManager.eventLocaleChanged -= LocaleChanged;
            LocaleManager.eventLocaleNeedsSubsitution -= LocaleNeedsSubsitution;
            LocaleManager.eventUIComponentLocaleChanged -= UIComponentLocaleChanged;
        }

        private void LocaleChanged()
        {
            Logger.LogDebug("LocaleChanged");
        }

        private LocaleManager.SubstitutionSetting[] LocaleNeedsSubsitution(string codeString)
        {
            Logger.LogDebug($"LocaleNeedsSubsitution > Code: {codeString}");

            return null;
        }

        private void UIComponentLocaleChanged()
        {
            Logger.LogDebug("UIComponentLocaleChanged");
        }
    }
}
