using PlaceHolders.DataPersistence;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


namespace PlaceHolders
{
  [CustomEditor(typeof(DataPersistenceManager), true)]
  public class DataPersistenceManagerInspector : UnityEditor.Editor
  {
    //[SerializeField] //if not needed for editor purposes.
    private VisualTreeAsset VisualTree = null;

    private VisualElement myInspector = null;

    private void OnEnable()
    {
      // Create a new VisualElement to be the root of our Inspector UI.
      myInspector = new VisualElement();

      if (VisualTree == null)
      {
          VisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets\\Editor\\DataPersistance\\DataPersistenceManagerInspector_UXML.uxml");
      }

      // Add the VisualTreeAsset instance.
      myInspector.Add(VisualTree.Instantiate());

      //Button button = myInspector.Querry<Button>("showInExplorer");
      Button button = myInspector.Q<Button>();
      button.RegisterCallback<ClickEvent>(ClickMessage);

      // Get references to the toggle and the encryption key field
      var encryptionInUse = myInspector.Q<Toggle>("useEncription");
      //var encryptionInUse = myInspector.Q<PropertyField>("useEncryption");
      var keyForEncryption = myInspector.Q<PropertyField>("encriptionKey");

      // Initialize the visibility based on the initial toggle value
      keyForEncryption.style.display = encryptionInUse.value ? DisplayStyle.Flex : DisplayStyle.None;

      // Register callback for changes to toggle the encryption key field visibility
      encryptionInUse.RegisterCallback<ChangeEvent<bool>>((changeEvent) =>
      {
        if (changeEvent.newValue)
        {
          keyForEncryption.style.display = DisplayStyle.Flex; // Show the field
        }
        else
        {
          keyForEncryption.style.display = DisplayStyle.None; // Hide the field
        }
      });
    }

    public override VisualElement CreateInspectorGUI()
    {
      return myInspector;
    }

    private void ClickMessage(ClickEvent evt)
    {
      // Reveal in Finder or Explorer the persistent data path
      EditorUtility.RevealInFinder($"{Application.persistentDataPath}/");
    }
  }
}