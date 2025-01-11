using UnityEditor;
using UnityEngine;

class ProjectInitialization
{
    [MenuItem("Tools/Project Initialization/Initialize", priority = 0)]
    static void DoSomething()
    {
        ProjectFilesEditor.CreateDefaultFolderStructure();
        BasicColorsEditor.CreateAllBasicColors();
        SceneConstantsEditor.CreateSceneConstantsClass();
        SystemsEditor.CreateSystemsPrefab();
    }
}