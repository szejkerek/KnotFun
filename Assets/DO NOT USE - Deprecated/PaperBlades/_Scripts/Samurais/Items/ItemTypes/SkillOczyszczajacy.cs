using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Oczyszczający")]
public class SkillOczyszczajacy : ItemSO
{
    public override void Use(IUnit target, IUnit origin)
    {
        SamuraiEffectsManager manager = origin.gameObject.GetComponent<SamuraiEffectsManager>();
        if (manager == null)
            return;

        if (!Cooldown.IsOffCooldown())
            return;
        manager.Roar();
        foreach (Samurai enemy in manager.Team)
        {
            if (!UnitInRange(origin, enemy, Range))
                continue;

            StatusManager statusManager = enemy.GetComponent<StatusManager>();
            statusManager.ApplyCleanse();
        }
        Cooldown.ResetTimers();
    }
}