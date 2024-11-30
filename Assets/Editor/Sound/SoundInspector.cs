using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements; // Ensure this is included for VisualTreeAsset and VisualElement


[CustomEditor(typeof(Sound), true)]
  public class SoundInspector : Editor
  {
    private VisualTreeAsset _VisualTree = null;
    private VisualElement _myInspector = null;

    private EnumField _soundType = null;
    private VisualElement _Settings3DContainer = null;
    private Toggle _spatialBlend = null;
    private VisualElement _toggleContainer = null;
    private Button _soundPreviewButton = null;
    //private EnumField _rolloffMode = null;
    private bool _sfxOrMusic = true;

    private AudioSource _previewer;
    private CustomAudioSettings _audioSettings = new CustomAudioSettings();
    

    private void OnEnable()
    {
      // Create the AudioSource for previewing
      _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();

      // Configure default audio settings
      _audioSettings.SfxVolume = 1;
      _audioSettings.MusicVolume = 1;




      // FAILED ATTEMPT, MIGHT COME BACK
      Editor audioSourceEditor = Editor.CreateEditor(_previewer);
      var list = audioSourceEditor.GetType().GetFields();
      //audioSourceEditor.serializedObject.Update();
      //MethodInfo methodDrawAudio3D = audioSourceEditor.GetType().GetMethod("Audio3DGUI", BindingFlags.NonPublic | BindingFlags.Instance);

      //methodDrawAudio3D.Invoke(audioSourceEditor, null);

      //audioSourceEditor.serializedObject.ApplyModifiedProperties();
    }


    public void OnDisable()
    {
      // Clean up the AudioSource when disabling the editor
      if (_previewer != null)
      {
        DestroyImmediate(_previewer.gameObject);
      }
    }


    public override VisualElement CreateInspectorGUI()
    {
      //return base.CreateInspectorGUI();

      // Create a new VisualElement to be the root of our Inspector UI.
      _myInspector = new VisualElement();

      if (_VisualTree == null)
      {
        _VisualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Systems/Audio/Editor/SoundInspector_UXML.uxml");
      }

      // Add the VisualTreeAsset instance.
      _myInspector.Add(_VisualTree.Instantiate());
      
      SetupUIElements();
      
      return _myInspector;
      // Binding Path of the "Settings3D" class' variables for the UXML editor:
      // <Settings3D>k__BackingField
      // <Settings3D>k__BackingField.spatialBlend
      // <Settings3D>k__BackingField.minDistance
      // <Settings3D>k__BackingField.maxDistance
    }

    private void SetupUIElements()
    {
  
  #region Querry fields

      // get sound type reference ("<EnumField>") and settings 3D container ("<VisualElement>")
      _soundType = _myInspector.Q<EnumField>("soundType");
      _soundType.value = (_soundType.value != null) ? _soundType.value : SoundType.SFX;
      _Settings3DContainer = _myInspector.Q<VisualElement>("Settings3D");

      // Get references to the toggle ("<Toggle>") and container ("<VisualElement>")
      _spatialBlend = _myInspector.Q<Toggle>("spatialBlend");
      _toggleContainer = _myInspector.Q<VisualElement>("toggleContainer");

      // Get rolloff reference ("<EnumField>")
      //_rolloffMode = _myInspector.Q<EnumField>("rolloffMode");

      // add button to preview the sound
      _soundPreviewButton = _myInspector.Q<Button>("soundPreview");

  #endregion


  #region Settings 3D Visibility Based On The SoundType && Distance Settings Visibility Based On The Toggle

      // Initialize the visibility based on the initial value
      _sfxOrMusic = (SoundType)(_soundType.value) == SoundType.SFX;
      _Settings3DContainer.style.display = _sfxOrMusic ? DisplayStyle.Flex : DisplayStyle.None;

      // Initialize the visibility based on the initial toggle value
      _spatialBlend.value = _sfxOrMusic ? _spatialBlend.value : false;
      _toggleContainer.style.display = _spatialBlend.value ? DisplayStyle.Flex : DisplayStyle.None;

  #endregion


  #region Register Callbacks

      // Register callback for changes to toggle the container visibility
      _soundType.RegisterCallback<ChangeEvent<Enum>>(Settings3DContainerCallback);

      // Register callback for changes to toggle the container visibility
      _spatialBlend.RegisterCallback<ChangeEvent<bool>>(ToggleContainerCallback);

      _soundPreviewButton.RegisterCallback<ClickEvent>(PlaySoundPreview);
  
  #endregion

    }


  #region Callbacks

    private void Settings3DContainerCallback(ChangeEvent<Enum> evt)
    {
      _sfxOrMusic = (SoundType)evt.newValue == SoundType.SFX;
      if (_sfxOrMusic)
      {
        _Settings3DContainer.style.display = DisplayStyle.Flex; // Show the field
      }
      else
      {
        _Settings3DContainer.style.display = DisplayStyle.None; // Hide the field
        _spatialBlend.value = false;
      }
    }
    private void ToggleContainerCallback(ChangeEvent<bool> evt)
    {
      if (evt.newValue && _sfxOrMusic)
      {
        _toggleContainer.style.display = DisplayStyle.Flex; // Show the field
      }
      else
      {
        _toggleContainer.style.display = DisplayStyle.None; // Hide the field
      }
    }

    private void PlaySoundPreview(ClickEvent evt)
    {
      // Call the Play function on the Sound object to preview audio
      ((Sound)target).Play(_previewer, _audioSettings);
    }

#endregion

  }
