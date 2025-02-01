using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MissingScriptUtility : MonoBehaviour
{
    [MenuItem("Tools/Search for missing scripts")]
    public static void SearchForMissingScripts()
    {
        FindMissingScriptsInScene();
        FindMissingScriptsInProject();
    }

    private static void FindMissingScriptsInProject()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();

        foreach (string prefabPath in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            foreach (Component component in prefab.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.LogError("Prefab found with missing script " + prefabPath, prefab);
                }
            }
        }
    }
    private static void FindMissingScriptsInScene()
    {
        foreach (GameObject gameObject in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.LogError("GameObject found with missing script " + gameObject.name);
                }
            }
        }
    }
}
