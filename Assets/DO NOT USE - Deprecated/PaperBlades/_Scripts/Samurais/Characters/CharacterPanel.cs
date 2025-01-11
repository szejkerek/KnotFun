using TMPro;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    public Character CurrentCharacter;

    [SerializeField] TMP_Text characterName;

    [SerializeField] StatsPanel statsPanel;

    [SerializeField] ItemView[] itemSlot = new ItemView[3];
    private void Awake()
    {
        gameObject.SetActive(false);
        CharacterView.OnCharacterSelected += SetupView;
    }

    private void OnDisable()
    {
        CharacterView.OnCharacterSelected -= SetupView;
    }

    public void SetupView(Character character)
    {
        gameObject.SetActive(true);

        CurrentCharacter = character;
        characterName.text = character.Name;
        statsPanel.Init(character);

        itemSlot[0].Init(character.Weapon);
        itemSlot[1].Init(character.Armor);
        itemSlot[2].Init(character.Skill);

    }
}
