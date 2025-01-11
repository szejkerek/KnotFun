using System.Collections.Generic;
using System.IO;

public static class AssetFolders
{
    public static string scriptsFolder = "__Scripts";
    public static string scriptsUtilityFolder = Path.Combine(scriptsFolder, "Utility");
    public static string colorsFolder = Path.Combine("Art", "Materials", "BasicColors");
    public static string resourcesFolder = "Resources";
    public static List<string> folderNames = new List<string>
    {
        scriptsFolder,
        scriptsUtilityFolder,
        "_Prefabs",
        "Art",
        Path.Combine("Art","2D"),
        Path.Combine("Art","3D"),
        Path.Combine("Art","Shaders"),
        Path.Combine("Art","VisualEffects"),
        Path.Combine("Art","Materials"),
        colorsFolder,
        "Audio",
        "Editor",
        "GameData",
        "Plugins",
        resourcesFolder,
        "Scenes",
        "Tests",
        Path.Combine("Tests","Playmode"),
        Path.Combine("Tests","Editor")
    };
}

