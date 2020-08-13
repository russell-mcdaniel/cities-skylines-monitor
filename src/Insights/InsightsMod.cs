using System;
using ICities;

namespace Insights
{
    public class InsightsMod : IUserMod
    {
        public string Name => "Insights";

        public string Description => "Gain insights from gameplay";

        public void OnEnabled()
        {
            Logger.Log("OnEnabled");
        }

        public void OnDisabled()
        {
            Logger.Log("OnDisabled");
        }
    }
}
