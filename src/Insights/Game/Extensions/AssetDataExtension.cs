using System.Collections.Generic;
using ICities;
using Insights.Logging;

namespace Insights.Game.Extensions
{
    public class AssetDataExtension : AssetDataExtensionBase
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(AssetDataExtension));

        public override void OnAssetLoaded(string name, object asset, Dictionary<string, byte[]> userData)
        {
            Logger.LogDebug($"OnAssetLoaded > Name: {name}");

            base.OnAssetLoaded(name, asset, userData);
        }

        public override void OnAssetSaved(string name, object asset, out Dictionary<string, byte[]> userData)
        {
            Logger.LogDebug($"OnAssetSaved > Name: {name}");

            base.OnAssetSaved(name, asset, out userData);
        }

        public override void OnCreated(IAssetData assetData)
        {
            Logger.LogDebug("OnCreated");

            base.OnCreated(assetData);
        }

        public override void OnReleased()
        {
            Logger.LogDebug("OnReleased");

            base.OnReleased();
        }
    }
}
