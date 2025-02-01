using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlaceHolders.EventChannel;
using PlaceHolders.SceneTransitions;

namespace PlaceHolders.Core
{
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Fires when a scene load begins.
        /// </summary>
        public static event Action OnSceneLoad;
        private int activeSceneIndex;
        private readonly List<Scene> scenesToUnload = new();

        private void Awake()
        {
            activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        }
        
        public void LoadScene(int sceneIndex)
        {
            if (activeSceneIndex == sceneIndex)
            {
                Debug.LogWarning($"[SceneLoader] Attempted to load the already active scene (Build Index: {sceneIndex}). Operation aborted.");
                return;
            }

            OnSceneLoad?.Invoke();
            
            AddScenesToUnload();
            
            activeSceneIndex = sceneIndex;
            
            AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
            loadOp.completed += _ =>
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
            };
            
            UnloadScenes();
        }
        
        private void AddScenesToUnload()
        {
            scenesToUnload.Clear();
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != "Initialization")
                {
                    Debug.Log($"[SceneLoader] Queueing scene to unload: {scene.name}");
                    scenesToUnload.Add(scene);
                }
            }
        }
        
        private void UnloadScenes()
        {
            foreach (Scene scene in scenesToUnload)
            {
                if (scene.isLoaded)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
            scenesToUnload.Clear();
        }
    }
}
