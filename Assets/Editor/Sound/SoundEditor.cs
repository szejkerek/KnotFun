using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sound), true)]
public class SoundEditor : Editor
{
    [SerializeField] private AudioSource _previewer;
    CustomAudioSettings audioSettings = new();

    public void OnEnable()
    {
        _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        audioSettings.SfxVolume = 1;
        audioSettings.MusicVolume = 1;
    }

    public void OnDisable()
    {
        DestroyImmediate(_previewer.gameObject);
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);

        EditorGUILayout.Space(15f);
        DrawLineSeparator();

        EditorGUILayout.LabelField("Editor Testing", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        audioSettings.SfxVolume = EditorGUILayout.Slider("SFX Volume", audioSettings.SfxVolume, 0f, 1f);
        audioSettings.MusicVolume = EditorGUILayout.Slider("Music Volume", audioSettings.MusicVolume, 0f, 1f);

        EditorGUILayout.Space();


        if (GUILayout.Button("Preview"))
        {
            ((Sound)target).Play(_previewer, audioSettings);
        }

        EditorGUI.EndDisabledGroup();
    }

    private void DrawLineSeparator()
    {
        // This creates a horizontal line using GUIStyle
        var rect = EditorGUILayout.GetControlRect(false, 1);
        EditorGUI.DrawRect(rect, Color.gray);
    }
}
