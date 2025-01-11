using TMPro;
using UnityEngine;

public class CharacterUIDisplay : MonoBehaviour
{
    [SerializeField] SamuraiImages viusals;
    [SerializeField] TMP_Text characterName;
    [SerializeField] GameObject textDisplay;
    public void Init(Character character)
    {
        viusals.SetActive(true);
        textDisplay.gameObject.SetActive(true);
        characterName.text = character.DisplayName;
        character.SamuraiVisuals.Apply(viusals, character);
    }
    public void Clear()
    {
        viusals.SetActive(false);
        textDisplay.gameObject.SetActive(false);
    }
    public void SetColor(Color color)
    {
        viusals.SetColor(color);
    }
}
