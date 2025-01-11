using System.Text;
using TMPro;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    Character currentCharacter;
    void Awake() => UpgradeableComponent.OnUpgrade += RefreshView;
    void OnDestroy() => UpgradeableComponent.OnUpgrade -= RefreshView;

    public void Init(Character character)
    {
        this.currentCharacter = character;
        RefreshView();
    }
    public void RefreshView()
    {
        text.text = DisplayText();
    }

    string DisplayText()
    {
        CharacterStats stats = currentCharacter.GetStats();
        StringBuilder sb = new StringBuilder();

        sb.AppendFormat("Damage: {0:0}\n", stats.Damage);
        sb.AppendFormat("Health: {0:0}/{1:0} \n", stats.MaxHealth - currentCharacter.LostHealth, stats.MaxHealth);
        sb.AppendFormat("Armor: {0:0}\n", stats.Armor);
        sb.AppendFormat("Movement Speed: {0:0}\n", stats.Speed);
        sb.AppendFormat("Crit Chance: {0:P0}\n", stats.CritChance);

        if (currentCharacter.PassiveEffects.Count > 0)
            sb.Append("\nPassive effects\n");

        foreach (PassiveEffectSO effectSO in currentCharacter.PassiveEffects)
        {
            sb.Append($"{effectSO.GetName()}: {effectSO.GetDesctiption()}\n");
        }

        return sb.ToString();
    }
}
