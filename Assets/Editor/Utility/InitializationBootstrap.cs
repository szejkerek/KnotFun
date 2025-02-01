using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlaceHolders.Editor
{
    public static class InitializationBootstrap
    {
        const string SceneName = "Initialization";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Execute()
        {
            for (int sceneIndex = 0; sceneIndex < SceneManager.sceneCount; ++sceneIndex)
            {
                var candidate = SceneManager.GetSceneAt(sceneIndex);
                if (candidate.name == SceneName)
                    return;
            }

            Debug.Log($"Loading {SceneName} scene.");
            SceneManager.LoadScene(SceneName, LoadSceneMode.Additive);
        }
    }
}
