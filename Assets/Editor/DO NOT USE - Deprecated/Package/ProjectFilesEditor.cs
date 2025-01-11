using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ProjectFilesEditor : EditorWindow
{
    [MenuItem("Tools/Project Initialization/Project Files Manager")]
    public static void ShowWindow()
    {
        GetWindow<ProjectFilesEditor>("Project Files Manager");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Default File Structure", EditorStyles.boldLabel);
        DisplayFolderList();

        if (GUILayout.Button("Create Default File Structure"))
        {
            CreateDefaultFolderStructure();
        }
    }

    public static void CreateDefaultFolderStructure()
    {
        foreach (string folderName in AssetFolders.folderNames)
        {
            CreateFolderStructure(folderName);
        }
        Debug.Log("Default project structure created.");
    }

    public static string CreateFolderStructure(string path)
    {
        string currentPath = "Assets";
        string[] folders = path.Split('\\');

        foreach (string folder in folders)
        {
            currentPath = Path.Combine(currentPath, folder);

            if (!AssetDatabase.IsValidFolder(currentPath))
            {
                AssetDatabase.CreateFolder(Path.GetDirectoryName(currentPath), Path.GetFileName(currentPath));
            }
        }

        AssetDatabase.Refresh();
        return currentPath;
    }

    public static bool IsPathValid(string path)
    {
        string currentPath = "Assets";
        string[] folders = path.Split('/');

        foreach (string folder in folders)
        {
            currentPath = Path.Combine(currentPath, folder);

            if (!AssetDatabase.IsValidFolder(currentPath))
            {
                return false;
            }
        }
        return true;
    }

    private void DisplayFolderList()
    {
        EditorGUI.indentLevel++;

        int listSize = EditorGUILayout.IntField("Folder Names Count", Mathf.Max(1, AssetFolders.folderNames.Count));

        AdjustListSize(listSize);

        for (int i = 0; i < AssetFolders.folderNames.Count; i++)
        {
            AssetFolders.folderNames[i] = EditorGUILayout.TextField($"Folder SavedFilename {i + 1}:", AssetFolders.folderNames[i]);
        }

        EditorGUI.indentLevel--;
    }

    private void AdjustListSize(int targetSize)
    {
        while (AssetFolders.folderNames.Count < targetSize)
        {
            AssetFolders.folderNames.Add("New Folder");
        }

        while (AssetFolders.folderNames.Count > targetSize)
        {
            AssetFolders.folderNames.RemoveAt(AssetFolders.folderNames.Count - 1);
        }
    }

}
