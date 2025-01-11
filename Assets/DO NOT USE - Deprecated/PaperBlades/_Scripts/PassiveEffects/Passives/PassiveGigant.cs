using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/Gigant")]
public class PassiveGigant : PassiveEffectSO
{
    public override string GetDesctiption()
    {
        return $"armor and damage are *200%, bigger body size";
    }

    public override string GetName()
    {
        return "Well fed";
    }

    public override void OnStart(SamuraiEffectsManager context)
    {
        context.transform.localScale *= 1.3f;
        context.Owner.temporaryStats.Armor += context.Owner.GetStats().Armor;
        context.Owner.temporaryStats.Damage += context.Owner.GetStats().Damage;
    }
}
