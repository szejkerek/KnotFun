using UnityEngine;

[CreateAssetMenu(menuName = "Character/Passsives/HealingOvertime")]
public class PassiveHealingOvertime : PassiveEffectSO
{
    public float healthPerTick;
    public float range;

    public override string GetDesctiption()
    {
        return $"heals nearby allies by {healthPerTick}";
    }

    public override string GetName()
    {
        return "Healer";
    }

    public override void OnUpdate(SamuraiEffectsManager context, float deltaTime)
    {
        foreach (Samurai team in context.Team)
        {
            if (team == null)
                return;

            if (Vector3.Distance(context.transform.position, team.transform.position) <= range)
            {
                //team.HealUnit(healthPerTick.ToInt());
            }
        }
    }
}
