using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    public static Action<Character> OnCharacterSelected;
    [SerializeField] Button characterBtn;
    //[SerializeField] Button returnBtn;
    [SerializeField] GameObject selectedBorder;
    [SerializeField] CharacterUIDisplay characterUIDisplay;

    Character character;

    private void Awake()
    {
        ResetView();
        selectedBorder.SetActive(false);
        characterBtn.onClick.AddListener(SelectCharacter);
        OnCharacterSelected += DisableBorder;

        //returnBtn.onClick.AddListener(ResetView);
    }
    public void EnableBorder()
    {
        selectedBorder.SetActive(true);
    }

    void DisableBorder(Character _)
    {
        selectedBorder.SetActive(false);
    }

    private void OnDisable()
    {
        OnCharacterSelected -= DisableBorder;
    }

    public void SelectCharacter()
    {
        OnCharacterSelected?.Invoke(character);
        selectedBorder.SetActive(true);
    }

    void ResetView()
    {
        //returnBtn.gameObject.SetActive(false);
        character = null;
        characterUIDisplay.Clear();
    }

    public void SetCharacter(Character character, bool isGeneral = false)
    {
        this.character = character;
        characterUIDisplay.Init(character);
        if (isGeneral)
            characterUIDisplay.SetColor(new Color(255f / 255, 73f / 255, 73f / 255, 255f / 255));
        //returnBtn.gameObject.SetActive(true);
    }


}
