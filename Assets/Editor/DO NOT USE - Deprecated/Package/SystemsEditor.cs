using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SystemsEditor : EditorWindow
{
    public static string PrefabName { get; } = "Systems";

    [MenuItem("Tools/Project Initialization/Create Systems Prefab")]
    public static void ShowWindow()
    {
        CreateSystemsPrefab();
    }

    public static void CreateSystemsPrefab()
    {
        string prefabPath = Path.Combine("Assets", AssetFolders.resourcesFolder, $"{PrefabName}.prefab");

        if (!AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)))
        {
            ProjectFilesEditor.CreateFolderStructure(AssetFolders.resourcesFolder);
            GameObject systemsObject = new GameObject(PrefabName);
            SetUpSystemsPrefab(systemsObject);
            PrefabUtility.SaveAsPrefabAsset(systemsObject, prefabPath);
            DestroyImmediate(systemsObject);
            Debug.Log($"{PrefabName}.prefab created successfully.");
        }
        else
        {
            Debug.Log($"{PrefabName}.prefab already exists.");
        }

        Selection.activeObject = null;
        AssetDatabase.Refresh();
    }

    private static void SetUpSystemsPrefab(GameObject prefab)
    {
        CreateAudioManager(prefab);
        CreateSceneLoader(prefab);
    }

    private static void CreateAudioManager(GameObject prefab)
    {
        prefab.AddComponent<AudioManagerOld>();
        GameObject musicSource = new GameObject("Music Source");
        musicSource.transform.SetParent(prefab.transform, false);
        musicSource.AddComponent<AudioManagerOld>();
    }

    private static void CreateSceneLoader(GameObject prefab)
    {
        prefab.AddComponent<SceneLoader>();
        prefab.AddComponent<FadeScreen>();
        GameObject canvas = CreateCanvas(prefab);
        CreateBlackScreen(canvas);
    }

    private static GameObject CreateBlackScreen(GameObject canvas)
    {
        GameObject blackImage = new GameObject("BlackImage");
        blackImage.transform.SetParent(canvas.transform, false);

        Image image = blackImage.AddComponent<Image>();
        blackImage.AddComponent<ImageFadeScreenTarget>();
        image.color = Color.black;
        RectTransform rectTransform = blackImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
        return blackImage;
    }

    private static GameObject CreateCanvas(GameObject prefab)
    {
        GameObject fader = new GameObject("ScreenFaderCanvas");

        Canvas canvas = fader.AddComponent<Canvas>();
        canvas.sortingOrder = 32767;
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = fader.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920, 1080);
        fader.transform.SetParent(prefab.transform, false);
        return fader;
    }
}

