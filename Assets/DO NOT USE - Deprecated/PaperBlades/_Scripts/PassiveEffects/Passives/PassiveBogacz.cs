using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/Bogacz")]
public class PassiveBogacz : PassiveEffectSO
{
    public float bonusDamagePerGold;
    public override string GetDesctiption()
    {
        return $"gives additional damage depending on actual gold ammount {bonusDamagePerGold * 100:0} per 100g";
    }

    public override string GetName()
    {
        return "GoldenBoy";
    }

    public override void OnStart(SamuraiEffectsManager context)
    {
        context.Owner.temporaryStats.Damage += (int)((float)SavableDataManager.Instance.data.playerResources.Money * bonusDamagePerGold);
    }

    public override void OnAttack(SamuraiEffectsManager context, IUnit target)
    {
        //TODO: Particle of coins in attack animation
    }

}
