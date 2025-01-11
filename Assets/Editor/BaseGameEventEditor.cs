using UnityEngine;
using UnityEditor;
using PlaceHolders.EventChannel;

[CustomEditor(typeof(BaseGameEvent<>), true)]
public class BaseGameEventEditor : Editor
{
    int intParameter = 0;
    string stringParameter = string.Empty;
    float floatParameter = 0f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Raise Event Test", EditorStyles.boldLabel);

        if (target is VoidEvent voidEvent)
        {
            if (GUILayout.Button("Raise"))
            {
                voidEvent.Raise();
            }
        }
        else if (target is IntEvent intEvent)
        {
            intParameter = EditorGUILayout.IntField("Parameter", intParameter);
            if (GUILayout.Button("Raise"))
            {
                intEvent.Raise(intParameter);
            }
        }
        else if (target is StringEvent stringEvent)
        {
            stringParameter = EditorGUILayout.TextField("Parameter", stringParameter);
            if (GUILayout.Button("Raise"))
            {
                stringEvent.Raise(stringParameter);
            }
        }
        else if (target is FloatEvent floatEvent)
        {
            floatParameter = EditorGUILayout.FloatField("Parameter", floatParameter);
            if (GUILayout.Button("Raise"))
            {
                floatEvent.Raise(floatParameter);
            }
        }
    }
}
