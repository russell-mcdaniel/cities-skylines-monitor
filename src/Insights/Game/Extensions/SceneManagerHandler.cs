using Insights.Logging;
using UnityEngine.SceneManagement;

namespace Insights.Game.Extensions
{
    public class SceneManagerHandler
    {
        protected InsightsLogger Logger { get; } = new InsightsLogger(typeof(SceneManagerHandler));

        public void Subscribe()
        {
            SceneManager.activeSceneChanged += ActiveSceneChanged;
            SceneManager.sceneLoaded += SceneLoaded;
            SceneManager.sceneUnloaded += SceneUnloaded;
        }

        public void Unsubscribe()
        {
            SceneManager.activeSceneChanged -= ActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneLoaded;
            SceneManager.sceneUnloaded -= SceneUnloaded;
        }

        private void ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
        {
            Logger.LogDebug($"ActiveSceneChanged > Previous: {previousActiveScene.name} | New: {newActiveScene.name}");
        }

        private void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Logger.LogDebug($"SceneLoaded > Scene: {scene.name} | Mode: {mode}");
        }

        private void SceneUnloaded(Scene scene)
        {
            Logger.LogDebug($"SceneUnloaded > Scene: {scene.name}");
        }
    }
}
