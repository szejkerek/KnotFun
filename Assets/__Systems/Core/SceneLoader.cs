using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using PlaceHolders.EventChannel;
using System;
using PlaceHolders.SceneTransitions;

namespace PlaceHolders.Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private VoidEvent saveEvent;
        [SerializeField] private VoidEvent loadEvent;

        [SerializeField] private FloatEvent progressEvent;
        [SerializeField] private IntEventListener intEventListener;
        private AsyncOperation loadLevelOperation;
        private SceneTransitionType activeTransitionMode = 0;

        private List<Scene> scenesToUnload = new List<Scene>();

        int activeScene = 0;
        void Awake()
        {
            activeScene = SceneManager.GetActiveScene().buildIndex;
            intEventListener.UnityEventResponse.AddListener(LoadScene);
            SceneManager.activeSceneChanged += HandleSceneChange;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.activeSceneChanged -= HandleSceneChange;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void LoadScene(int scene)
        {
            saveEvent.Raise();
            SceneTransitionType transitionMode = SceneTransitionType.Circle;
            LoadSceneMode mode = LoadSceneMode.Additive;
            bool progressScreen = true;
            

            if(activeScene == scene) return;
            AddScenesToUnload();
            activeScene = scene;
            activeTransitionMode = transitionMode;
            loadLevelOperation = SceneManager.LoadSceneAsync(scene, mode);
            loadLevelOperation.allowSceneActivation = false;

            loadLevelOperation.completed += SetActiveScene;
            StartCoroutine(Exit(progressScreen));
            if (progressScreen)StartCoroutine(TrackLoadingProgress());
            UnloadScenes();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode) => loadEvent.Raise();

        private void LoadGame()
        {
            throw new NotImplementedException();
        }

        private void SetActiveScene(AsyncOperation operation)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(activeScene));
        }

        private void AddScenesToUnload()
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != "Initialization")
                {
                    Debug.Log("Added scene to unload = " + scene.name);
                    scenesToUnload.Add(scene);
                }
            }
        }

        private void UnloadScenes()
        {           
            if (scenesToUnload != null)
            {
                for (int i = 0; i < scenesToUnload.Count; ++i)
                {
                    SceneManager.UnloadSceneAsync(scenesToUnload[i]);
                }
            }
            scenesToUnload.Clear();
        }


        private IEnumerator TrackLoadingProgress()
        {
            float totalProgress = 0;
            while (totalProgress < 0.9f)
            {

                //totalProgress = loadLevelOperation.progress;
                totalProgress += 0.1f; //for testing TODO: delete this
                progressEvent.Raise(totalProgress);
                Debug.Log("progress value =" + totalProgress );

                yield return new WaitForSeconds(0.1f); //For testing TODO:  change value to null or 0.1f
                //yield return null;
            }
        }


        private IEnumerator Enter()
        {
            Debug.Log("enter");
            yield return StartCoroutine(CoreManager.SceneTransitionManager.MakeTransition(activeTransitionMode, true));
            loadLevelOperation = null;
        }

        private void HandleSceneChange(Scene oldScene, Scene newScene)
        {
            Debug.Log($"Old: {oldScene.name}, New: {newScene.name}");
            if (activeTransitionMode != 0 && newScene.buildIndex == activeScene)
            {
                Debug.Log("handle");
                StartCoroutine(Enter());
            }
                
        }

        private IEnumerator Exit(bool progressScreen)
        {
            Debug.Log("exit");
            yield return StartCoroutine(CoreManager.SceneTransitionManager.MakeTransition(activeTransitionMode, false, progressScreen));
            loadLevelOperation.allowSceneActivation = true;
        }
    }


}
