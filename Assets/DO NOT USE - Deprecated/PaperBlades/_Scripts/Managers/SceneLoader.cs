using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] float SceneChangeTime = 0.75f;
    public Action OnSceneChanged;
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        FadeScreen fadeScreen = FadeScreen.Instance;

        if (fadeScreen == null)
        {
            SceneManager.LoadSceneAsync(sceneIndex);
            Debug.LogError("Couldn't get screen fader in scene!");
        }
        else
        {
            fadeScreen.FadeOut(SceneChangeTime);
            yield return new WaitForSeconds(SceneChangeTime);

            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            float timer = 0f;
            while (timer <= 0.1f && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            OnSceneChanged?.Invoke();
            CursorManager.Instance.SetCursorState(CursorState.Default);
            operation.allowSceneActivation = true;
            fadeScreen.FadeIn(SceneChangeTime);
        }
    }
}
