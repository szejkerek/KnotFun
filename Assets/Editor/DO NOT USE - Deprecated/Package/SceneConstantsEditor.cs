using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneConstantsEditor : EditorWindow
{
    private static string className = "SceneConstants";

    [MenuItem("Tools/Project Initialization/Create Scene Constants")]
    public static void ShowWindow()
    {
        GetWindow<SceneConstantsEditor>("Scene Constants");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Scene Constants", EditorStyles.boldLabel);
        className = EditorGUILayout.TextField("Class SavedFilename:", className);

        if (GUILayout.Button("Create/OnUpdate Scene Constants file"))
        {
            CreateSceneConstantsClass();
        }
    }

    public static void CreateSceneConstantsClass()
    {
        string currentPath = ProjectFilesEditor.CreateFolderStructure(AssetFolders.scriptsUtilityFolder);
        string filePath = Path.Combine(currentPath, $"{className}.cs");

        File.WriteAllText(filePath, GenerateClassContent());

        AssetDatabase.Refresh();
        Debug.Log($"Successfully created {className}");
    }

    public static string GenerateClassContent()
    {
        string content = $"public static class {className}\n";
        content += "{\n";

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            int sceneIndex = i;

            content += $"    public const int {ValidVariableName(sceneName)} = {sceneIndex};\n";
        }

        content += "}\n";

        return content;
    }


    private static string ValidVariableName(string name)
    {
        name = new string(name
            .Select(c => char.IsLetterOrDigit(c) || c == '_' ? c : '_')
            .ToArray());

        if (!char.IsLetter(name[0]) && name[0] != '_')
        {
            name = '_' + name;
        }

        return name;
    }
}
