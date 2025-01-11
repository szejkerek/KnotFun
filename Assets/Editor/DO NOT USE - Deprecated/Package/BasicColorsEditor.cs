using UnityEditor;
using UnityEngine;

public class BasicColorsEditor
{
    [MenuItem("Tools/Project Initialization/Create Materials")]
    static void CreateMaterials()
    {
        CreateAllBasicColors();
        AssetDatabase.Refresh();
    }

    public static void CreateAllBasicColors()
    {
        string path = ProjectFilesEditor.CreateFolderStructure(AssetFolders.colorsFolder); 

        CreateMaterial(path, "Red", Color.red);
        CreateMaterial(path, "Green", Color.green);
        CreateMaterial(path, "Blue", Color.blue);
        CreateMaterial(path, "Yellow", Color.yellow);
        CreateMaterial(path, "Cyan", Color.cyan);
        CreateMaterial(path, "Magenta", Color.magenta);
        CreateMaterial(path, "White", Color.white);
        CreateMaterial(path, "Black", Color.black);
        CreateMaterial(path, "Gray", Color.gray);
        CreateMaterial(path, "Orange", new Color(1.0f, 0.5f, 0.0f));
        CreateMaterial(path, "Purple", new Color(0.5f, 0.0f, 0.5f));
        CreateMaterial(path, "Brown", new Color(0.6f, 0.4f, 0.2f));
        CreateMaterial(path, "Pink", new Color(1.0f, 0.6f, 0.75f));
        CreateMaterial(path, "Turquoise", new Color(0.25f, 0.88f, 0.82f));
        CreateMaterial(path, "Lime", new Color(0.75f, 1.0f, 0.0f));
        CreateMaterial(path, "Indigo", new Color(0.29f, 0.0f, 0.51f));
        CreateMaterial(path, "Salmon", new Color(0.98f, 0.5f, 0.45f));
    }

    static void CreateMaterial(string folderPath, string materialName, Color color)
    {
        Material material = new Material(Shader.Find("Standard"));
        material.color = color;
        string materialPath = $"{folderPath}/{materialName}.mat";
        AssetDatabase.CreateAsset(material, materialPath);
    }
}
