using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/Truciciel")]
public class PassiveTruciciel : PassiveEffectSO
{
    public int dmgPerTick;
    public int ticks;
    public override string GetDesctiption()
    {
        return $"attacs give poison effect, it gives the target {dmgPerTick} damage every 0.5 second";
    }

    public override string GetName()
    {
        return "Poisoner";
    }

    public override void OnAttack(SamuraiEffectsManager context, IUnit target)
    {
        //TODO: Particle of coins in attack animation
        target.gameObject.GetComponent<StatusManager>().ApplyPoison(dmgPerTick, ticks);
    }

}
