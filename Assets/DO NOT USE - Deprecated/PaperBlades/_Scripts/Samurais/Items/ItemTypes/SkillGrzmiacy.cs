using UnityEngine;

[CreateAssetMenu(menuName = "Character/Skill/Grzmiący")]
public class SkillGrzmiacy : ItemSO
{
    public float howLongItLast;
    public override void Use(IUnit target, IUnit origin)
    {
        SamuraiEffectsManager manager = origin.gameObject.GetComponent<SamuraiEffectsManager>();
        if (manager == null)
            return;

        if (!Cooldown.IsOffCooldown())
            return;
        manager.Roar();
        foreach (Enemy enemy in manager.Enemies)
        {
            if (!UnitInRange(origin, enemy, Range))
                continue;

            StatusManager statusManager = enemy.GetComponent<StatusManager>();
            statusManager.ApplyStun(howLongItLast);
        }
        Cooldown.ResetTimers();
    }
}
