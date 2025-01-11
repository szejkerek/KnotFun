using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/Szlachcic")]
public class PassiveSzlachcic : PassiveEffectSO
{
    public int GoldPerLevel;

    public override string GetDesctiption()
    {
        return $"every visited island gives +{GoldPerLevel} gold";
    }

    public override string GetName()
    {
        return "Noble";
    }

    public override void OnStart(SamuraiEffectsManager context)
    {
        SavableDataManager.Instance.data.playerResources.AddMoney(GoldPerLevel);
    }
}