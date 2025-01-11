using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneEnumEditor : EditorWindow
{
    private static string className = "SceneEnum";

    [MenuItem("Tools/Project Initialization/Create Scene Enum")]
    public static void ShowWindow()
    {
        GetWindow<SceneEnumEditor>("Scene Enum");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Scene Enum", EditorStyles.boldLabel);
        className = EditorGUILayout.TextField("Class SavedFilename:", className);

        if (GUILayout.Button("Create/OnUpdate Scene Enum file"))
        {
            CreateSceneEnum();
        }
    }

    public static void CreateSceneEnum()
    {
        string currentPath = ProjectFilesEditor.CreateFolderStructure("Systems/Core");
        string filePath = Path.Combine(currentPath, $"{className}.cs");

        File.WriteAllText(filePath, GenerateEnumContent());

        AssetDatabase.Refresh();
        Debug.Log($"Successfully created {className}");
    }

    public static string GenerateEnumContent()
    {
        string content = $"public enum {className}\n";
        content += "{\n";

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            int sceneIndex = i;

            content += $"    {ValidVariableName(sceneName)},\n";
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

